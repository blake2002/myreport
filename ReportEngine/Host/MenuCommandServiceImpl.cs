using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.Design;
using System.Windows.Forms;
using System.Drawing;

namespace Host
{
    //class MenuCommandServiceImpl : IMenuCommandService
    //{

    //    //- this ServiceProvider is the DesignsurfaceExt2 instance 
    //    //- passed as paramter inside the ctor
    //    IServiceProvider _serviceProvider = null;

    //    MenuCommandService _menuCommandService = null;

    //    public MenuCommandServiceImpl(IServiceProvider serviceProvider)
    //    {
    //        this._serviceProvider = serviceProvider;
    //        _menuCommandService = new MenuCommandService(serviceProvider);
    //    }

    //    public void ShowContextMenu(CommandID menuID, int x, int y)
    //    {
    //        ContextMenu contextMenu = new ContextMenu();

    //        // Add the standard commands CUT/COPY/PASTE/DELETE
    //        MenuCommand command = FindCommand(StandardCommands.Cut);
    //        if (command != null)
    //        {
    //            MenuItem menuItem = new MenuItem("Cut", new EventHandler(OnMenuClicked));
    //            menuItem.Tag = command;
    //            contextMenu.MenuItems.Add(menuItem);
    //        }
    //        command = FindCommand(StandardCommands.Copy);
    //        if (command != null)
    //        {
    //            MenuItem menuItem = new MenuItem("Copy", new EventHandler(OnMenuClicked));
    //            menuItem.Tag = command;
    //            contextMenu.MenuItems.Add(menuItem);
    //        }
    //        command = FindCommand(StandardCommands.Paste);
    //        if (command != null)
    //        {
    //            MenuItem menuItem = new MenuItem("Paste", new EventHandler(OnMenuClicked));
    //            menuItem.Tag = command;
    //            contextMenu.MenuItems.Add(menuItem);
    //        }
    //        command = FindCommand(StandardCommands.Delete);
    //        if (command != null)
    //        {
    //            MenuItem menuItem = new MenuItem("Delete", new EventHandler(OnMenuClicked));
    //            menuItem.Tag = command;
    //            contextMenu.MenuItems.Add(menuItem);
    //        }

    //        //- Show the contexteMenu
    //        DesignSurface surface = (DesignSurface)_serviceProvider;
    //        Control viewService = (Control)surface.View;

    //        if (viewService != null)
    //        {
    //            contextMenu.Show(viewService, viewService.PointToClient(new Point(x, y)));
    //        }
    //    }

    //    //- Management of the selections of the contexteMenu
    //    private void OnMenuClicked(object sender, EventArgs e)
    //    {
    //        MenuItem menuItem = sender as MenuItem;
    //        if (menuItem != null && menuItem.Tag is MenuCommand)
    //        {
    //            MenuCommand command = menuItem.Tag as MenuCommand;
    //            command.Invoke();
    //        }
    //    }


    //    public void AddCommand(MenuCommand command)
    //    {
    //        _menuCommandService.AddCommand(command);
    //    }


    //    public void AddVerb(DesignerVerb verb)
    //    {
    //        _menuCommandService.AddVerb(verb);
    //    }


    //    public MenuCommand FindCommand(CommandID commandID)
    //    {
    //        return _menuCommandService.FindCommand(commandID);
    //    }


    //    public bool GlobalInvoke(CommandID commandID)
    //    {
    //        return _menuCommandService.GlobalInvoke(commandID);
    //    }


    //    public void RemoveCommand(MenuCommand command)
    //    {
    //        _menuCommandService.RemoveCommand(command);
    //    }


    //    public void RemoveVerb(DesignerVerb verb)
    //    {
    //        _menuCommandService.RemoveVerb(verb);
    //    }


    //    public DesignerVerbCollection Verbs
    //    {
    //        get
    //        {
    //            return _menuCommandService.Verbs;
    //        }
    //    }
    //}

    public class MenuCommandServiceImpl : MenuCommandService
    {
        public static readonly string strguid = "90e7a6bb-242e-4811-9240-e3bafa92d420";
        public static readonly CommandID PropertyCommand = new CommandID(Guid.Parse(strguid),512);

        IServiceProvider _serviceProvider = null;
        private ISelectionService _selectionService = null;

        //MenuCommandService _menuCommandService = null;

        public DesignSurface DesignSurface = null;

        public MenuCommandServiceImpl(IServiceProvider serviceProvider)
            : base(serviceProvider)
        {
            this._serviceProvider = serviceProvider;

            _selectionService = (ISelectionService)(this._serviceProvider.GetService(typeof(ISelectionService)));

            //_menuCommandService = new MenuCommandService(serviceProvider);

            MenuCommand undoCommand = new MenuCommand(new EventHandler(ExecuteUndo), StandardCommands.Undo);
            base.AddCommand(undoCommand);

            MenuCommand redoCommand = new MenuCommand(new EventHandler(ExecuteRedo), StandardCommands.Redo);
            base.AddCommand(redoCommand);
        }

        void ExecuteUndo(object sender, EventArgs e)
        {
            UndoEngineExt undoEngine = GetService(typeof(UndoEngine)) as UndoEngineExt;
            if (undoEngine != null)
                undoEngine.Undo();
        }

        void ExecuteRedo(object sender, EventArgs e)
        {
            UndoEngineExt undoEngine = GetService(typeof(UndoEngine)) as UndoEngineExt;
            if (undoEngine != null)
                undoEngine.Redo();
        }

        public bool Paste()
        {
            return base.GlobalInvoke(StandardCommands.Paste);
        }

        public override bool GlobalInvoke(CommandID commandID)
        {
            // 删除命令
            // 拷贝命令
            // 剪切命令
            if (StandardCommands.Delete == commandID
                || StandardCommands.Copy == commandID
                || StandardCommands.Cut == commandID)
            {
                ISelectionService selectionService = GetService(typeof(ISelectionService)) as ISelectionService;
                if (selectionService != null)
                {
                    System.Collections.ICollection ic = selectionService.GetSelectedComponents();
                    System.Collections.IEnumerator ie = ic.GetEnumerator();
                    while(ie.MoveNext())
                    {
                        if(PMS.Libraries.ToolControls.PMSPublicInfo.CurrentPrjInfo.IsIndependentDesignerMode)
                        {
                            object[] obs = ie.Current.GetType().GetCustomAttributes(false);
                            foreach (object ob in obs)
                            {
                                // 不删除设计时的基本控件带属性标签MES.PublicInterface.MESBasicDesignerControlAttributeAttribute
                                if (ob.ToString() == "MES.PublicInterface.MESBasicDesignerControlAttributeAttribute")
                                    return false;

                                //以下为测试设计时设置Enable属性代码，无效
                                //if (ob is uctest.SimpleControlDesigner)
                                //    (ob as uctest.SimpleControlDesigner).Enabled = false;
                            }
                        }
                    }

                }
            }
            else if(StandardCommands.Paste == commandID)
            {
                int msgID = PMS.Libraries.ToolControls.PMSPublicInfo.Message.USER_DOCOUTLINEREFRESHSWITCH;
                IntPtr handle = PMS.Libraries.ToolControls.PMSPublicInfo.Message.DocumentOutlineFormHandle;

                PMS.Libraries.ToolControls.PMSPublicInfo.Message.SendMsgToMainForm(handle, msgID, IntPtr.Zero, IntPtr.Zero);
                IDesignerHost host1 = (IDesignerHost)this.GetService(typeof(IDesignerHost));
                DesignerTransaction tran1 = host1.CreateTransaction("Paste");
                bool bret = base.GlobalInvoke(StandardCommands.Paste);
                Point oldPoint = Point.Empty;
                if (_selectionService != null)
                {
                    Control ctrl = _selectionService.PrimarySelection as Control;
                    if (null != ctrl)
                        oldPoint = ctrl.Location;
                }
                System.Collections.ICollection selectedComponents1 = null;
                if (_selectionService != null)
                {
                    Point newPoint = _lastPoint;
                    int xOffset = newPoint.X - oldPoint.X;
                    int yOffset = newPoint.Y - oldPoint.Y;
                    selectedComponents1 = _selectionService.GetSelectedComponents();
                    _selectionService.SetSelectedComponents(null);
                    foreach (object ob in selectedComponents1)
                    {
                        if (ob is Control)
                        {
                            IComponentChangeService componentChangeService = (IComponentChangeService)this._serviceProvider.GetService(typeof(IComponentChangeService));
                            Control co = ob as Control;
                            componentChangeService.OnComponentChanging(co, null);
                            Point oldValue = co.Location;
                            Point newValue = new Point(co.Location.X + xOffset, co.Location.Y + yOffset);
                            co.Location = newValue;
                            componentChangeService.OnComponentChanged(co, null, oldValue, newValue);
                        }
                    }
                    _selectionService.SetSelectedComponents(selectedComponents1);
                }
                tran1.Commit();
                PMS.Libraries.ToolControls.PMSPublicInfo.Message.SendMsgToMainForm(handle, msgID, IntPtr.Zero, (IntPtr)1);
                return bret;
            }
            else if (PropertyCommand == commandID)
            {
                int msgID = PMS.Libraries.ToolControls.PMSPublicInfo.Message.USER_SHOWPROPERTYGRID;
                IntPtr handle = PMS.Libraries.ToolControls.PMSPublicInfo.Message.PMSDeveloperControlHandle;
                PMS.Libraries.ToolControls.PMSPublicInfo.Message.SendMsgToMainForm(handle, msgID, IntPtr.Zero, IntPtr.Zero);
            }
 	        return base.GlobalInvoke(commandID);
        }

        private Point _lastPoint = new Point(0,0);
        public override void  ShowContextMenu(CommandID menuID, int x, int y)
        {
            ContextMenu contextMenu = new ContextMenu();

            // Add the standard commands CUT/COPY/PASTE/DELETE/Property
            MenuCommand command = FindCommand(StandardCommands.Cut);
            if (command != null)
            {
                MenuItem menuItem = new MenuItem("Cut", new EventHandler(OnMenuClicked));
                menuItem.Tag = command;
                contextMenu.MenuItems.Add(menuItem);
            }
            command = FindCommand(StandardCommands.Copy);
            if (command != null)
            {
                MenuItem menuItem = new MenuItem("Copy", new EventHandler(OnMenuClicked));
                menuItem.Tag = command;
                contextMenu.MenuItems.Add(menuItem);
            }
            command = FindCommand(StandardCommands.Paste);
            if (command != null)
            {
                MenuItem menuItem = new MenuItem("Paste", new EventHandler(OnMenuClicked));
                menuItem.Tag = command;
                contextMenu.MenuItems.Add(menuItem);
            }
            command = FindCommand(StandardCommands.Delete);
            if (command != null)
            {
                MenuItem menuItem = new MenuItem("Delete", new EventHandler(OnMenuClicked));
                menuItem.Tag = command;
                contextMenu.MenuItems.Add(menuItem);
            }
            //command = FindCommand(PropertyCommand);
            //if (command != null)
            {
                MenuItem menuItem = new MenuItem("Property", new EventHandler(OnMenuClicked));
                menuItem.Tag = PropertyCommand;
                contextMenu.MenuItems.Add(menuItem);
            }

            //- Show the contexteMenu
            //DesignSurface surface = (DesignSurface)_serviceProvider;
            Control viewService = (Control)DesignSurface.View;

            if (viewService != null)
            {
                _lastPoint = viewService.PointToClient(new Point(x, y));
                contextMenu.Show(viewService, _lastPoint);
            }
        }

        //- Management of the selections of the contexteMenu
        private void OnMenuClicked(object sender, EventArgs e)
        {
            MenuItem menuItem = sender as MenuItem;
            if (menuItem != null && menuItem.Tag is MenuCommand)
            {
                MenuCommand command = menuItem.Tag as MenuCommand;
                GlobalInvoke(command.CommandID);
                //command.Invoke();
            }
            else if(menuItem != null && menuItem.Tag is CommandID)
            {
                CommandID command = menuItem.Tag as CommandID;
                GlobalInvoke(command);
            }
        }

    }
}
