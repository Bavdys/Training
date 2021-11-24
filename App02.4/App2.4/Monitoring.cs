using System;
using System.Collections.Generic;
using System.IO;

namespace App2._4
{
    public class Monitoring
    {
        private FileSystemWatcher _systemWatcher;

        private void OnChanged(object sender, FileSystemEventArgs e)
        {
            OnChangeValue(new FileChangeEventArgs(e.FullPath));
        }

        protected virtual void OnChangeValue(FileChangeEventArgs e)
        {
            FileChange?.Invoke(this, e);
        }

        public List<IAuditor> Audits { get; set; } = new List<IAuditor>();

        public void Initialization(ISetting setting)
        {
            Audits=new List<IAuditor>(setting.SettingMonitoring());
        }
        public void Start()
        {
            foreach (var audit in Audits)
            {
                audit.StartChecking();
            }
        }
        public void Stop()
        {
            foreach(var audit in Audits)
            {
                audit.StopChecking();
            }
        }
        public void WatchingFile(string path)
        {
            string directoryPath = Path.GetDirectoryName(path);
            string filePath = Path.GetFileName(path);

            _systemWatcher = new FileSystemWatcher(directoryPath, filePath);

            _systemWatcher.Changed += new FileSystemEventHandler(OnChanged);
            _systemWatcher.EnableRaisingEvents = true;
        }

        public event EventHandler<FileChangeEventArgs> FileChange;
    }
}
