using AudioStream.Forms;
using NAudio.Wave;
using System.IO.Pipes;

namespace AudioStream {

    internal static class Program {

        public static WaveOutEvent? outputDevice;
        public static RawSourceWaveStream stream = new(Properties.Resources.empty_1c, new WaveFormat());

        public static string appId = "Local\\01701E72-2007-498D-8B20-CBE0183F8E72";
        public static Mutex mutex = new(false, appId);

        public static MainForm? form;
        public static NamedPipeServerStream? pipe;

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() {
            // Lock mutex and catch duplicate instances
            if (!mutex.WaitOne(0)) {
                using NamedPipeClientStream client = new(".", appId, PipeDirection.Out);

                client.Connect(2500);
                client.Close();
                client.Dispose();

                return;
            }

            pipe = new(appId, PipeDirection.In);
            pipe.BeginWaitForConnection(OnPipeConnection, pipe);

            // Initialize the output device
            outputDevice = new();
            outputDevice.Init(stream);
            outputDevice.Play();

            // Initialize display configurations
            ApplicationConfiguration.Initialize();

            // Create the form
            form = new();

            // Show the form if start minimized is disabled
            if (!SettingsManager.StartMinimized) {
                form.Show();
            }

            // Otherwise we'll do some non-legit stuff to start the form on the main thread without showing it
            else {
                form.Opacity = 0;
                form.ShowInTaskbar = false;
                form.Show();
                form.Hide();
                form.Opacity = 1;
                form.ShowInTaskbar = true;
            }

            // Start the application
            Application.Run();

            // Clean up
            mutex.ReleaseMutex();
            pipe.Close();
            pipe.Dispose();
        }

        /// <summary>
        /// Handles pipe connections from duplicate instances, which signal that we should show the main form.
        /// </summary>
        /// <param name="result"></param>
        private static void OnPipeConnection(IAsyncResult result) {
            try {
                if (form != null) {
                    if (form.InvokeRequired) {
                        form.BeginInvoke(new MethodInvoker(() => {
                            form.Show();
                        }));
                    }
                    else {
                        form.Show();
                    }
                }
            }
            catch (ObjectDisposedException) {
                return;
            }
            catch (Exception) {}
            finally {
                // Restart the pipe
                pipe?.Dispose();
                pipe = new(appId, PipeDirection.In);
                pipe.BeginWaitForConnection(OnPipeConnection, pipe);
            }
        }

    }

}