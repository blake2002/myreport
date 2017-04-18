using System;
using System.Windows.Forms;
using System.Drawing;
using System.Security.Policy;
using System.IO;
using System.Linq;
using System.IO.Ports;
using System.Threading;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Xml;
using PMS.Libraries.ToolControls.PMSPublicInfo;

namespace PMS.Libraries.ToolControls
{
	public class test:Form
	{
		static bool flag = false;
		static bool isBreak = false;

		public test ()
		{
			this.Size = new Size (800, 600);
			cmb.Location = new Point (20, 0);
			cmb.Size = new Size (500, 100);
			var files = Directory.GetFiles (@"/home/administrator/Documents/").Where (f => f.EndsWith (".rpt"));
			cmb.Items.Clear ();
			foreach (var f in files) {
				cmb.Items.Add (f);
			}
			btn.Text = "start";
			btn.Location = new Point (20, 50);
			btn.Size = new Size (200, 200);
			CurrentPrjInfo.CurrentEnvironment = MESEnvironment.MESReportServer;
			Action<string,string> action = (filePath, fileName) => {
				
				MESReportRun.Instance.RunReportServer (filePath, fileName, "123", true, string.Empty);

			};
			btn.Click += (s, o) => {
				try {
					Task.Run (() => {
						string filePath = cmb.Text.Trim ();
						//int endindex = filePath.IndexOf (".rpt");
						int index = filePath.LastIndexOf (System.IO.Path.DirectorySeparatorChar);
						if (!string.IsNullOrEmpty (filePath) && index != -1) {
							string fileName = filePath.Substring ((index + 1)); 


							int i = 0;
							while (flag) {
								//flag = false;
								this.Invoke (action, filePath, fileName);
								i++;
								Console.WriteLine (i);
								Thread.Sleep (5000);
							}
						}
					});


				} catch (System.Exception ex) {
					System.Diagnostics.Debug.WriteLine (ex.Message);
				}

			};
			btn1.Text = "stop";
			btn1.Location = new Point (300, 50);
			btn1.Size = new Size (200, 200);
			btn1.Click += (s, o) => {
				try {
					flag = !flag;
//					var file = @"/home/administrator/Desktop/aa.xml";
//					XmlDocument doc = new XmlDocument ();
//					doc.Load (file);
//					var loader = new Loader.RunTimeLoader (doc);
//					Form f = new Form ();
//					f.Name = "Form1";
//					loader.BaseForm = f;
//					f.AutoScroll = true;
//					loader.LoadToRuntimeForm ();
//					Form f1 = loader.GetRuntimeForm ();
//					f1.Show ();

				} catch (System.Exception ex) {
					System.Diagnostics.Debug.WriteLine (ex.Message);
				}

			};

			btn2.Text = "gc";
			btn2.Location = new Point (20, 300);
			btn2.Size = new Size (200, 200);
			btn2.Click += (s, o) => {
				try {

					GC.Collect ();

				} catch (System.Exception ex) {
					System.Diagnostics.Debug.WriteLine (ex.Message);
				}

			};
			this.Controls.Add (cmb);
			this.Controls.Add (btn);
			this.Controls.Add (btn1);
			this.Controls.Add (btn2);
		}

		Button btn = new Button ();
		Button btn1 = new Button ();
		Button btn2 = new Button ();
		//TextBox text = new TextBox ();
		ComboBox cmb = new ComboBox ();
	}
}

