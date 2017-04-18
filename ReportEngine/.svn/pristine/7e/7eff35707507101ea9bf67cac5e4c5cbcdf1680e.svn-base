// Decompiled with JetBrains decompiler
// Type: System.Windows.Forms.Design.Behavior.Behavior
// Assembly: System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: D9A8513D-61B3-4428-A683-7E8B21B75D84
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\System.Design.dll

using System;
using System.ComponentModel.Design;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Design.Behavior;

namespace MES.Controls.Design
{
	/// <summary>
	/// 表示由 <see cref="T:System.Windows.Forms.Design.Behavior.BehaviorService"/> 管理的 <see cref="T:System.Windows.Forms.Design.Behavior.Behavior"/> 对象。
	/// </summary>
	public class MyBehavior:Behavior
	{
		private bool callParentBehavior;
		private BehaviorService bhvSvc;

		private Behavior GetNextBehavior
		{
			get
			{
				if (this.bhvSvc != null)
					return this.bhvSvc.GetNextBehavior(this);
				return (Behavior) null;
			}
		}

		/// <summary>
		/// 获取应为此行为显示的光标。
		/// </summary>
		/// 
		/// <returns>
		/// 一个 <see cref="T:System.Windows.Forms.Cursor"/> 表示应为此行为显示的光标。
		/// </returns>
		public virtual Cursor Cursor
		{
			get
			{
				return Cursors.Default;
			}
		}

		/// <summary>
		/// 获取一个值，该值指示是否应该禁用 <see cref="T:System.ComponentModel.Design.MenuCommand"/> 对象。
		/// </summary>
		/// 
		/// <returns>
		/// 如果在此 <see cref="T:System.Windows.Forms.Design.Behavior.Behavior"/> 处于活动状态时，该设计器接收的所有 <see cref="T:System.ComponentModel.Design.MenuCommand"/> 对象的状态应设置为 Enabled = false，则为 true；否则为 false。
		/// </returns>
		public virtual bool DisableAllCommands
		{
			get
			{
				if (this.callParentBehavior && this.GetNextBehavior != null)
					return this.GetNextBehavior.DisableAllCommands;
				return false;
			}
		}

		/// <summary>
		/// 初始化 <see cref="T:System.Windows.Forms.Design.Behavior.Behavior"/> 类的新实例。
		/// </summary>
		protected MyBehavior()
		{
		}

		/// <summary>
		/// 用给定的 <see cref="T:System.Windows.Forms.Design.Behavior.BehaviorService"/> 初始化 <see cref="T:System.Windows.Forms.Design.Behavior.Behavior"/> 类的新实例。
		/// </summary>
		/// <param name="callParentBehavior">如果应该调用父行为（如果存在），则为 true；否则为 false。</param><param name="behaviorService">要使用的 <see cref="T:System.Windows.Forms.Design.Behavior.BehaviorService"/>。  </param><exception cref="T:System.ArgumentNullException"><paramref name="callParentBehavior"/> 为 true 且 <paramref name="behaviorService"/> 为 null。</exception>
		protected MyBehavior(bool callParentBehavior, BehaviorService behaviorService)
		{
			if (callParentBehavior && behaviorService == null)
				throw new ArgumentException("behaviorService");
			this.callParentBehavior = callParentBehavior;
			this.bhvSvc = behaviorService;
		}

		/// <summary>
		/// 截获命令。
		/// </summary>
		/// 
		/// <returns>
		/// <see cref="T:System.ComponentModel.Design.MenuCommand"/>。默认情况下，<see cref="M:System.Windows.Forms.Design.Behavior.Behavior.FindCommand(System.ComponentModel.Design.CommandID)"/> 返回 null。
		/// </returns>
		/// <param name="commandId">一个 <see cref="T:System.ComponentModel.Design.CommandID"/> 对象。</param>
		public virtual MenuCommand FindCommand(CommandID commandId)
		{
			try
			{
				if (this.callParentBehavior && this.GetNextBehavior != null)
					return this.GetNextBehavior.FindCommand(commandId);
				return (MenuCommand) null;
			}
			catch
			{
				return (MenuCommand) null;
			}
		}

		private bool GlyphIsValid(Glyph g)
		{
			if (g != null && g.Behavior != null)
				return g.Behavior != this;
			return false;
		}

		/// <summary>
		/// 在丢失鼠标捕获时由装饰器窗口调用。
		/// </summary>
		/// <param name="g">一个 <see cref="T:System.Windows.Forms.Design.Behavior.Glyph"/>，拖放行为对其调用。</param><param name="e">一个 <see cref="T:System.EventArgs"/>，其中包含事件数据。</param>
		public virtual void OnLoseCapture(Glyph g, EventArgs e)
		{
			if (this.callParentBehavior && this.GetNextBehavior != null)
			{
				this.GetNextBehavior.OnLoseCapture(g, e);
			}
			else
			{
				if (!this.GlyphIsValid(g))
					return;
				g.Behavior.OnLoseCapture(g, e);
			}
		}

		/// <summary>
		/// 在任何双击消息进入 <see cref="T:System.Windows.Forms.Design.Behavior.BehaviorService"/> 的装饰器窗口时调用。
		/// </summary>
		/// 
		/// <returns>
		/// 如果消息已得到处理，则为 true；否则为 false。
		/// </returns>
		/// <param name="g"><see cref="T:System.Windows.Forms.Design.Behavior.Glyph"/>。</param><param name="button">一个 <see cref="T:System.Windows.Forms.MouseButtons"/> 值，指示单击了哪个按钮。</param><param name="mouseLoc">发生单击的位置。</param>
		public virtual bool OnMouseDoubleClick(Glyph g, MouseButtons button, Point mouseLoc)
		{
			if (this.callParentBehavior && this.GetNextBehavior != null)
				return this.GetNextBehavior.OnMouseDoubleClick(g, button, mouseLoc);
			if (this.GlyphIsValid(g))
				return g.Behavior.OnMouseDoubleClick(g, button, mouseLoc);
			return false;
		}

		/// <summary>
		/// 在任何鼠标按下消息进入 <see cref="T:System.Windows.Forms.Design.Behavior.BehaviorService"/> 的装饰器窗口时调用。
		/// </summary>
		/// 
		/// <returns>
		/// 如果消息已得到处理，则为 true；否则为 false。
		/// </returns>
		/// <param name="g"><see cref="T:System.Windows.Forms.Design.Behavior.Glyph"/>。</param><param name="button">一个 <see cref="T:System.Windows.Forms.MouseButtons"/> 值，指示单击了哪个按钮。</param><param name="mouseLoc">发生单击的位置。</param>
		public virtual bool OnMouseDown(Glyph g, MouseButtons button, Point mouseLoc)
		{
			if (this.callParentBehavior && this.GetNextBehavior != null)
				return this.GetNextBehavior.OnMouseDown(g, button, mouseLoc);
			if (this.GlyphIsValid(g))
				return g.Behavior.OnMouseDown(g, button, mouseLoc);
			return false;
		}

		/// <summary>
		/// 在任何鼠标输入消息进入 <see cref="T:System.Windows.Forms.Design.Behavior.BehaviorService"/> 的装饰器窗口时调用。
		/// </summary>
		/// 
		/// <returns>
		/// 如果消息已得到处理，则为 true；否则为 false。
		/// </returns>
		/// <param name="g"><see cref="T:System.Windows.Forms.Design.Behavior.Glyph"/>。</param>
		public virtual bool OnMouseEnter(Glyph g)
		{
			if (this.callParentBehavior && this.GetNextBehavior != null)
				return this.GetNextBehavior.OnMouseEnter(g);
			if (this.GlyphIsValid(g))
				return g.Behavior.OnMouseEnter(g);
			return false;
		}

		/// <summary>
		/// 在任何鼠标悬停消息进入 <see cref="T:System.Windows.Forms.Design.Behavior.BehaviorService"/> 的装饰器窗口时调用。
		/// </summary>
		/// 
		/// <returns>
		/// 如果消息已得到处理，则为 true；否则为 false。
		/// </returns>
		/// <param name="g"><see cref="T:System.Windows.Forms.Design.Behavior.Glyph"/>。</param><param name="mouseLoc">发生悬停的位置。</param>
		public virtual bool OnMouseHover(Glyph g, Point mouseLoc)
		{
			if (this.callParentBehavior && this.GetNextBehavior != null)
				return this.GetNextBehavior.OnMouseHover(g, mouseLoc);
			if (this.GlyphIsValid(g))
				return g.Behavior.OnMouseHover(g, mouseLoc);
			return false;
		}

		/// <summary>
		/// 在任何鼠标离开消息进入 <see cref="T:System.Windows.Forms.Design.Behavior.BehaviorService"/> 的装饰器窗口时调用。
		/// </summary>
		/// 
		/// <returns>
		/// 如果消息已得到处理，则为 true；否则为 false。
		/// </returns>
		/// <param name="g"><see cref="T:System.Windows.Forms.Design.Behavior.Glyph"/>。</param>
		public virtual bool OnMouseLeave(Glyph g)
		{
			if (this.callParentBehavior && this.GetNextBehavior != null)
				return this.GetNextBehavior.OnMouseLeave(g);
			if (this.GlyphIsValid(g))
				return g.Behavior.OnMouseLeave(g);
			return false;
		}

		/// <summary>
		/// 在任何鼠标移动消息进入 <see cref="T:System.Windows.Forms.Design.Behavior.BehaviorService"/> 的装饰器窗口时调用。
		/// </summary>
		/// 
		/// <returns>
		/// 如果消息已得到处理，则为 true；否则为 false。
		/// </returns>
		/// <param name="g"><see cref="T:System.Windows.Forms.Design.Behavior.Glyph"/>。</param><param name="button">一个 <see cref="T:System.Windows.Forms.MouseButtons"/> 值，指示单击了哪个按钮。</param><param name="mouseLoc">发生移动的位置。</param>
		public virtual bool OnMouseMove(Glyph g, MouseButtons button, Point mouseLoc)
		{
			if (this.callParentBehavior && this.GetNextBehavior != null)
				return this.GetNextBehavior.OnMouseMove(g, button, mouseLoc);
			if (this.GlyphIsValid(g))
				return g.Behavior.OnMouseMove(g, button, mouseLoc);
			return false;
		}

		/// <summary>
		/// 在任何鼠标弹起消息进入 <see cref="T:System.Windows.Forms.Design.Behavior.BehaviorService"/> 的装饰器窗口时调用。
		/// </summary>
		/// 
		/// <returns>
		/// 如果消息已得到处理，则为 true；否则为 false。
		/// </returns>
		/// <param name="g"><see cref="T:System.Windows.Forms.Design.Behavior.Glyph"/>。</param><param name="button">一个 <see cref="T:System.Windows.Forms.MouseButtons"/> 值，指示单击了哪个按钮。</param>
		public virtual bool OnMouseUp(Glyph g, MouseButtons button)
		{
			if (this.callParentBehavior && this.GetNextBehavior != null)
				return this.GetNextBehavior.OnMouseUp(g, button);
			if (this.GlyphIsValid(g))
				return g.Behavior.OnMouseUp(g, button);
			return false;
		}

		/// <summary>
		/// 允许自定义的拖放行为。
		/// </summary>
		/// <param name="g">一个 <see cref="T:System.Windows.Forms.Design.Behavior.Glyph"/> 对象，拖放行为对其调用。</param><param name="e">包含事件数据的 <see cref="T:System.Windows.Forms.DragEventArgs"/>。</param>
		public virtual void OnDragDrop(Glyph g, DragEventArgs e)
		{
			if (this.callParentBehavior && this.GetNextBehavior != null)
			{
				this.GetNextBehavior.OnDragDrop(g, e);
			}
			else
			{
				if (!this.GlyphIsValid(g))
					return;
				g.Behavior.OnDragDrop(g, e);
			}
		}

		/// <summary>
		/// 允许自定义的拖入行为。
		/// </summary>
		/// <param name="g">一个 <see cref="T:System.Windows.Forms.Design.Behavior.Glyph"/>，拖入行为对其调用。</param><param name="e">包含事件数据的 <see cref="T:System.Windows.Forms.DragEventArgs"/>。</param>
		public virtual void OnDragEnter(Glyph g, DragEventArgs e)
		{
			if (this.callParentBehavior && this.GetNextBehavior != null)
			{
				this.GetNextBehavior.OnDragEnter(g, e);
			}
			else
			{
				if (!this.GlyphIsValid(g))
					return;
				g.Behavior.OnDragEnter(g, e);
			}
		}

		/// <summary>
		/// 允许自定义的拖离行为。
		/// </summary>
		/// <param name="g">一个 <see cref="T:System.Windows.Forms.Design.Behavior.Glyph"/>，拖离行为对其调用。</param><param name="e">包含事件数据的 <see cref="T:System.Windows.Forms.DragEventArgs"/>。</param>
		public virtual void OnDragLeave(Glyph g, EventArgs e)
		{
			if (this.callParentBehavior && this.GetNextBehavior != null)
			{
				this.GetNextBehavior.OnDragLeave(g, e);
			}
			else
			{
				if (!this.GlyphIsValid(g))
					return;
				g.Behavior.OnDragLeave(g, e);
			}
		}

		/// <summary>
		/// 允许自定义的拖过行为。
		/// </summary>
		/// <param name="g">一个 <see cref="T:System.Windows.Forms.Design.Behavior.Glyph"/>，拖过行为对其调用。</param><param name="e">包含事件数据的 <see cref="T:System.Windows.Forms.DragEventArgs"/>。</param>
		public virtual void OnDragOver(Glyph g, DragEventArgs e)
		{
			if (this.callParentBehavior && this.GetNextBehavior != null)
				this.GetNextBehavior.OnDragOver(g, e);
			else if (this.GlyphIsValid(g))
			{
				g.Behavior.OnDragOver(g, e);
			}
			else
			{
				if (e.Effect == DragDropEffects.None)
					return;
				e.Effect = Control.ModifierKeys == Keys.Control ? DragDropEffects.Copy : DragDropEffects.Move;
			}
		}

		/// <summary>
		/// 允许自定义的拖放反馈行为。
		/// </summary>
		/// <param name="g">一个 <see cref="T:System.Windows.Forms.Design.Behavior.Glyph"/>，拖放行为对其调用。</param><param name="e">包含事件数据的 <see cref="T:System.Windows.Forms.GiveFeedbackEventArgs"/>。</param>
		public virtual void OnGiveFeedback(Glyph g, GiveFeedbackEventArgs e)
		{
			if (this.callParentBehavior && this.GetNextBehavior != null)
			{
				this.GetNextBehavior.OnGiveFeedback(g, e);
			}
			else
			{
				if (!this.GlyphIsValid(g))
					return;
				g.Behavior.OnGiveFeedback(g, e);
			}
		}

		/// <summary>
		/// 将此拖放事件从装饰器窗口发送到相应的 <see cref="T:System.Windows.Forms.Design.Behavior.Behavior"/> 或经过命中测试的 <see cref="T:System.Windows.Forms.Design.Behavior.Glyph"/>。
		/// </summary>
		/// <param name="g"><see cref="T:System.Windows.Forms.Design.Behavior.Glyph"/>。</param><param name="e">包含事件数据的 <see cref="T:System.Windows.Forms.QueryContinueDragEventArgs"/>。</param>
		public virtual void OnQueryContinueDrag(Glyph g, QueryContinueDragEventArgs e)
		{
			if (this.callParentBehavior && this.GetNextBehavior != null)
			{
				this.GetNextBehavior.OnQueryContinueDrag(g, e);
			}
			else
			{
				if (!this.GlyphIsValid(g))
					return;
				g.Behavior.OnQueryContinueDrag(g, e);
			}
		}
	}
}
