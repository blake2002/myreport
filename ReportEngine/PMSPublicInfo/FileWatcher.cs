using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.IO;
using System.Threading;

//-------------------------------------------
/*
 * 文件监控类
 * lyj,2012/9/28
 */
//-------------------------------------------
namespace PMS.Libraries.ToolControls.PMSPublicInfo
{
    //////////////////////////////////////////////////////////////////////////
    ///MESFileSystemWather测试用例
    //////////////////////////////////////////////////////////////////////////

    //////////////////////////////////////////////////////////////////////////
    //static void Main(string[] args)
    //{
    //    MESFileSystemWather myWather = new MESFileSystemWather(@"C:\test", "*.txt");
    //    myWather.OnChanged += new FileSystemEventHandler(OnChanged);
    //    myWather.OnCreated += new FileSystemEventHandler(OnCreated);
    //    myWather.OnRenamed += new RenamedEventHandler(OnRenamed);
    //    myWather.OnDeleted += new FileSystemEventHandler(OnDeleted);
    //    myWather.Start();
    //    //由于是控制台程序，加个输入避免主线程执行完毕，看不到监控效果
    //    Console.ReadKey();
    //}

    //private static void OnCreated(object source, FileSystemEventArgs e)
    //{
    //    Console.WriteLine("文件新建事件处理逻辑");
    //}

    //private static void OnChanged(object source, FileSystemEventArgs e)
    //{
    //    Console.WriteLine("文件改变事件处理逻辑");
    //}

    //private static void OnDeleted(object source, FileSystemEventArgs e)
    //{
    //    Console.WriteLine("文件删除事件处理逻辑");
    //}

    //private static void OnRenamed(object source, RenamedEventArgs e)
    //{
    //    Console.WriteLine("文件重命名事件处理逻辑");
    //}

    public delegate void Completed(string key);

    public class MESFileSystemWather
    {
        private FileSystemWatcher fsWather;
        private Hashtable hstbWather;
        public event RenamedEventHandler OnRenamed;
        public event FileSystemEventHandler OnChanged;
        public event FileSystemEventHandler OnCreated;
        public event FileSystemEventHandler OnDeleted;

        /// <summary> 
        /// 构造函数 
        /// </summary> 
        /// <param name="path">要监控的路径</param> 
        public MESFileSystemWather(string path, string filter)
        {
            if (!Directory.Exists(path))
            {
                throw new Exception("找不到路径：" + path);
            }
            hstbWather = new Hashtable();
            fsWather = new FileSystemWatcher(path);
            // 是否监控子目录
            fsWather.IncludeSubdirectories = false;
            fsWather.Filter = filter;
            fsWather.Renamed += new RenamedEventHandler(fsWather_Renamed);
            fsWather.Changed += new FileSystemEventHandler(fsWather_Changed);
            fsWather.Created += new FileSystemEventHandler(fsWather_Created);
            fsWather.Deleted += new FileSystemEventHandler(fsWather_Deleted);
        }

        /// <summary> 
        /// 开始监控 
        /// </summary> 
        public void Start()
        {
            fsWather.EnableRaisingEvents = true;
        }

        /// <summary> 
        /// 停止监控 
        /// </summary> 
        public void Stop()
        {
            fsWather.EnableRaisingEvents = false;
        }

        /// <summary> 
        /// filesystemWatcher 本身的事件通知处理过程 
        /// </summary> 
        /// <param name="sender"></param> 
        /// <param name="e"></param> 
        private void fsWather_Renamed(object sender, RenamedEventArgs e)
        {
            lock (hstbWather)
            {
                hstbWather.Add(e.FullPath, e);
            }
            WatcherProcess watcherProcess = new WatcherProcess(sender, e);
            watcherProcess.OnCompleted += new Completed(WatcherProcess_OnCompleted);
            watcherProcess.OnRenamed += new RenamedEventHandler(WatcherProcess_OnRenamed);
            Thread thread = new Thread(watcherProcess.Process);
            thread.Start();
        }

        private void WatcherProcess_OnRenamed(object sender, RenamedEventArgs e)
        {
            OnRenamed(sender, e);
        }

        private void fsWather_Created(object sender, FileSystemEventArgs e)
        {
            lock (hstbWather)
            {
                hstbWather.Add(e.FullPath, e);
            }
            WatcherProcess watcherProcess = new WatcherProcess(sender, e);
            watcherProcess.OnCompleted += new Completed(WatcherProcess_OnCompleted);
            watcherProcess.OnCreated += new FileSystemEventHandler(WatcherProcess_OnCreated);
            Thread threadDeal = new Thread(watcherProcess.Process);
            threadDeal.Start();
        }

        private void WatcherProcess_OnCreated(object sender, FileSystemEventArgs e)
        {
            OnCreated(sender, e);
        }

        private void fsWather_Deleted(object sender, FileSystemEventArgs e)
        {
            lock (hstbWather)
            {
                hstbWather.Add(e.FullPath, e);
            }
            WatcherProcess watcherProcess = new WatcherProcess(sender, e);
            watcherProcess.OnCompleted += new Completed(WatcherProcess_OnCompleted);
            watcherProcess.OnDeleted += new FileSystemEventHandler(WatcherProcess_OnDeleted);
            Thread tdDeal = new Thread(watcherProcess.Process);
            tdDeal.Start();
        }

        private void WatcherProcess_OnDeleted(object sender, FileSystemEventArgs e)
        {
            OnDeleted(sender, e);
        }

        private void fsWather_Changed(object sender, FileSystemEventArgs e)
        {
            if (e.ChangeType == WatcherChangeTypes.Changed)
            {
                if (hstbWather.ContainsKey(e.FullPath))
                {
                    WatcherChangeTypes oldType = ((FileSystemEventArgs)hstbWather[e.FullPath]).ChangeType;
                    if (oldType == WatcherChangeTypes.Created || oldType == WatcherChangeTypes.Changed)
                    {
                        return;
                    }
                }
            }

            lock (hstbWather)
            {
                hstbWather.Add(e.FullPath, e);
            }
            WatcherProcess watcherProcess = new WatcherProcess(sender, e);
            watcherProcess.OnCompleted += new Completed(WatcherProcess_OnCompleted);
            watcherProcess.OnChanged += new FileSystemEventHandler(WatcherProcess_OnChanged);
            Thread thread = new Thread(watcherProcess.Process);
            thread.Start();
        }

        private void WatcherProcess_OnChanged(object sender, FileSystemEventArgs e)
        {
            OnChanged(sender, e);
        }

        public void WatcherProcess_OnCompleted(string key)
        {
            lock (hstbWather)
            {
                hstbWather.Remove(key);
            }
        }
    }

    public class WatcherProcess
    {
        private object sender;
        private object eParam;
        public event RenamedEventHandler OnRenamed;
        public event FileSystemEventHandler OnChanged;
        public event FileSystemEventHandler OnCreated;
        public event FileSystemEventHandler OnDeleted;
        public event Completed OnCompleted;

        public WatcherProcess(object sender, object eParam)
        {

            this.sender = sender;

            this.eParam = eParam;

        }

        public void Process()
        {

            if (eParam.GetType() == typeof(RenamedEventArgs))
            {

                OnRenamed(sender, (RenamedEventArgs)eParam);

                OnCompleted(((RenamedEventArgs)eParam).FullPath);

            }

            else
            {

                FileSystemEventArgs e = (FileSystemEventArgs)eParam;

                if (e.ChangeType == WatcherChangeTypes.Created)
                {

                    OnCreated(sender, e);

                    OnCompleted(e.FullPath);

                }

                else if (e.ChangeType == WatcherChangeTypes.Changed)
                {

                    OnChanged(sender, e);

                    OnCompleted(e.FullPath);

                }

                else if (e.ChangeType == WatcherChangeTypes.Deleted)
                {

                    OnDeleted(sender, e);

                    OnCompleted(e.FullPath);

                }

                else
                {

                    OnCompleted(e.FullPath);

                }

            }

        }
    }
    
}
