using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace PMS.Libraries.ToolControls.PMSPublicInfo
{
    public class ShortCutKeyManager
    {
        private List<ShortCutKey> _shortKeys = null;

        public ShortCutKeyManager()
        {
            _shortKeys = new List<ShortCutKey>();
        }

        public ShortCutKeyManager(Control ctrl)
        {
            _shortKeys = new List<ShortCutKey>();
            if (null != ctrl)
            {
                ctrl.KeyDown += new KeyEventHandler(ctrl_KeyDown);
            }
        }

        void ctrl_KeyDown(object sender, KeyEventArgs e)
        {
            ProcessKeyDown();
        }

        [DllImport("user32.dll", EntryPoint = "GetKeyState")]
        public static extern short GetKeyState(int keyCode);

        public bool ProcessKeyDown()
        {
            if (null != _shortKeys)
            {
                for (int i = 0; i < _shortKeys.Count; i++)
                {
                    ShortCutKey sk = _shortKeys[i];
                    if (null == sk)
                    {
                        continue;
                    }
                    bool isKeyDown = true;
                    for (int j = 0; j < sk.KeyCodes.Length; j++)
                    {
                        isKeyDown &= ((GetKeyState(sk.KeyCodes[j]) & 0x8000) != 0);
                    }
                    if (isKeyDown)
                    {
                        if (null != sk.Action)
                        {
                            sk.Action();
                            return true;
                        }
                        return false;
                    }
                }
            }
            return false;
        }

        public void AddShortCutKey(ShortCutKey sk)
        {
            if (null != _shortKeys)
            {
                _shortKeys.Add(sk);
            }
        }

        public void RemoveShortCutKey(ShortCutKey sk)
        {
            if (null != _shortKeys && null != sk)
            {
                _shortKeys.Remove(sk);
            }
        }

        public void RemoveShortCutKey(int[] keyCodes)
        {
            ShortCutKey sk = GetShortCutKey(keyCodes);
            if (null != sk)
            {
                RemoveShortCutKey(sk);
            }
        }

        private ShortCutKey GetShortCutKey(int[] keyCodes)
        {
            if (null == _shortKeys || null == keyCodes
                                   || keyCodes.Length == 0)
            {
                return null;
            }

            foreach (ShortCutKey sk in _shortKeys)
            {
                if (null == sk || null == sk.KeyCodes || sk.KeyCodes.Length != keyCodes.Length)
                {
                    continue;
                }

                bool[] array = new bool[keyCodes.Length];
                for (int i = 0; i < keyCodes.Length; i++)
                {
                    int code = keyCodes[i];
                    for (int j = 0; j < sk.KeyCodes.Length; j++)
                    {
                        if (code == sk.KeyCodes[j])
                        {
                            array[j] = true;
                            break;
                        }
                    }
                }
                bool isMatch = true;
                foreach (bool b in array)
                {
                    isMatch &= b;
                }
                if (isMatch)
                {
                    return sk;
                }
            }


            return null;
        }

        public void Clear()
        {
            if (null != _shortKeys)
            {
                _shortKeys.Clear();
            }
        }

    }

    public class ShortCutKey
    {
        public int[] KeyCodes
        {
            get;
            set;
        }

        public Action Action
        {
            get;
            set;
        }

        /// <summary>
        /// 快捷键
        /// </summary>
        /// <param name="keyCodes">A-Z,a-z,0-9用ASCII值</param>
        /// <param name="action">键值被按下的响应方法</param>
        public ShortCutKey(int[] keyCodes, Action action)
        {
            KeyCodes = keyCodes;
            Action = action;
        }
    }
}
