// --------------------------------
// <copyright file="Form1.cs" company="Thomas Loehlein">
//     WebDavNet - A WebDAV client
//     Copyright (C) 2009 - Thomas Loehlein
//     This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
//     This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
//     You should have received a copy of the GNU General Public License along with this program; if not, see http://www.gnu.org/licenses/.
// </copyright>
// <author>Thomas Loehlein</author>
// <email>thomas.loehlein@gmail.com</email>
// ---------------------------------

using System;
using System.Windows.Forms;
using WebDav;

namespace WebDavWin
{
    public partial class FormWebDav : Form
    {
        private WebDavManager _webDavManager = new WebDavManager();
        private AsyncHandler _asyncHandler;

        public FormWebDav()
        {
            InitializeComponent();

            textBoxLocalFile.Text = @"C:\test.avi";
            textBoxRemoteUri.Text = "http://localhost:8080/webdav/test.avi";

            _webDavManager.DownloadProgressEvent += _webDavManager_DownloadProgressEvent;
        }

        private void _webDavManager_DownloadProgressEvent(object sender, ProgressEventArgs args)
        {
            progressBar1.Maximum = (int)args.TotalBytes;
            progressBar1.Value = (int)args.TransferredBytes;
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            _asyncHandler = _webDavManager.DownloadFileAsync(new Uri(textBoxRemoteUri.Text), textBoxLocalFile.Text);
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            if (_asyncHandler != null)
                _asyncHandler.Abort();
        }
    }
}
