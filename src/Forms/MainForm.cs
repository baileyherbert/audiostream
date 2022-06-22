using AudioStream.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AudioStream.Forms {

    public partial class MainForm : Form {

        public MainForm() {
            InitializeComponent();

            this.notifyIcon1.Visible = SettingsManager.EnableTrayIcon;

            this.enableTrayIconToolStripMenuItem.Checked = SettingsManager.EnableTrayIcon;
            this.enableTrayIconToolStripMenuItem1.Checked = SettingsManager.EnableTrayIcon;

            this.enableOnStartupToolStripMenuItem.Checked = SettingsManager.EnableStartup;
            this.enableOnStartupToolStripMenuItem1.Checked = SettingsManager.EnableStartup;

            this.startMinimizedToolStripMenuItem.Checked = SettingsManager.StartMinimized;
            this.startMinimizedToolStripMenuItem1.Checked = SettingsManager.StartMinimized;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e) {
            this.notifyIcon1.Visible = false;
            Application.Exit();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e) {
            this.Show();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e) {
            if (e.CloseReason == CloseReason.UserClosing) {
                e.Cancel = true;
                this.Hide();
            }
            else {
                notifyIcon1.Visible = false;
                Application.Exit();
            }
        }

        private void MainForm_Load(object sender, EventArgs e) {
            this.richTextBox1.Text = this.richTextBox1.Text.Replace("{version}", Application.ProductVersion.ToString());
        }

        private void richTextBox1_LinkClicked(object sender, LinkClickedEventArgs e) {
            if (e.LinkText != null) {
                Process.Start(new ProcessStartInfo() {
                    UseShellExecute = true,
                    FileName = e.LinkText,
                });
            }
        }

        private void repositoryToolStripMenuItem_Click(object sender, EventArgs e) {
            Process.Start(new ProcessStartInfo() {
                UseShellExecute = true,
                FileName = "https://github.com/baileyherbert/audiostream",
            });
        }

        private void onToggleTrayIcon(object sender, EventArgs e) {
            ToolStripMenuItem menuItem = (ToolStripMenuItem)sender;
            bool enabled = menuItem.Checked;

            this.enableTrayIconToolStripMenuItem.Checked = enabled;
            this.enableTrayIconToolStripMenuItem1.Checked = enabled;
            this.notifyIcon1.Visible = enabled;

            SettingsManager.EnableTrayIcon = enabled;
        }

        private void onToggleStartup(object sender, EventArgs e) {
            ToolStripMenuItem menuItem = (ToolStripMenuItem)sender;
            bool enabled = menuItem.Checked;

            this.enableOnStartupToolStripMenuItem.Checked = enabled;
            this.enableOnStartupToolStripMenuItem1.Checked = enabled;

            SettingsManager.EnableStartup = enabled;
        }

        private void onToggleMinimized(object sender, EventArgs e) {
            ToolStripMenuItem menuItem = (ToolStripMenuItem)sender;
            bool enabled = menuItem.Checked;

            this.startMinimizedToolStripMenuItem.Checked = enabled;
            this.startMinimizedToolStripMenuItem1.Checked = enabled;

            SettingsManager.StartMinimized = enabled;
        }

        private void notifyIcon1_DoubleClick(object sender, EventArgs e) {
            this.Show();
        }

        private void updatesToolStripMenuItem_Click(object sender, EventArgs e) {
            Process.Start(new ProcessStartInfo() {
                UseShellExecute = true,
                FileName = "https://github.com/baileyherbert/audiostream/releases",
            });
        }
    }

}
