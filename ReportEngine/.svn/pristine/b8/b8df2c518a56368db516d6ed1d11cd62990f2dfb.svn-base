
using System.Linq;
using System.Collections.Generic;
using System;
using System.Windows.Forms;
using System.ComponentModel;

namespace PMS.Libraries.ToolControls.PMSPublicInfo.LinqToWindowsForms
{
    public static class TreeExtensions
    {
        /// <summary>
        /// Returns a collection of descendant elements.
        /// </summary>
	    public static IEnumerable<Control> Descendants(this Control item)
        {
            ILinqTree<Control> adapter = new WindowsFormsTreeAdapter(item);
            foreach (var child in adapter.Children())
            {
                yield return child;

                foreach (var grandChild in child.Descendants())
                {
                    yield return grandChild;
                }
            }
        }    
           
        /// <summary>
        /// Returns a collection containing this element and all descendant elements.
        /// </summary>
	    public static IEnumerable<Control> DescendantsAndSelf(this Control item)
        {            
            yield return item;

            foreach (var child in item.Descendants())
            {
                yield return child;
            }
        }
        
        /// <summary>
        /// Returns a collection of ancestor elements.
        /// </summary>
	    public static IEnumerable<Control> Ancestors(this Control item)
        {
            ILinqTree<Control> adapter = new WindowsFormsTreeAdapter(item);
            
            var parent = adapter.Parent;
            while(parent != null)
            {
                yield return parent;
                adapter = new WindowsFormsTreeAdapter(parent);
                parent = adapter.Parent;
            }
        } 
        
        /// <summary>
        /// Returns a collection containing this element and all ancestor elements.
        /// </summary>
	    public static IEnumerable<Control> AncestorsAndSelf(this Control item)
        {
            yield return item;

            foreach (var ancestor in item.Ancestors())
            {
                yield return ancestor;
            }
        }
        
        /// <summary>
        /// Returns a collection of child elements.
        /// </summary>
        public static IEnumerable<Control> Elements(this Control item)
        {
            ILinqTree<Control> adapter = new WindowsFormsTreeAdapter(item);
            foreach (var child in adapter.Children())
            {
                yield return child;
            }
        }
        
        /// <summary>
        /// Returns a collection of the sibling elements before this node, in document order.
        /// </summary>
	    public static IEnumerable<Control> ElementsBeforeSelf(this Control item)
        {
			if (item.Ancestors().FirstOrDefault()==null)
				yield break;
            foreach (var child in item.Ancestors().First().Elements())
            {
				if (child.Equals(item))
					break;
                yield return child;                
            }
        }
        
        /// <summary>
        /// Returns a collection of the elements after this node, in document order.
        /// </summary>
	    public static IEnumerable<Control> ElementsAfterSelf(this Control item)
        {
			if (item.Ancestors().FirstOrDefault()==null)
				yield break;
            bool afterSelf = false;
            foreach (var child in item.Ancestors().First().Elements())
            {
				if (afterSelf)
					yield return child;                
                
                if (child.Equals(item))
					afterSelf=true;
            }
        }
        
        /// <summary>
        /// Returns a collection containing this element and all child elements.
        /// </summary>
	    public static IEnumerable<Control> ElementsAndSelf(this Control item)
        {
            yield return item;

            foreach (var child in item.Elements())
            {
                yield return child;
            }
        }
      
        /// <summary>
        /// Returns a collection of descendant elements which match the given type.
        /// </summary>
	    public static IEnumerable<Control> Descendants<T>(this Control item)
        {
            return item.Descendants().Where(i => i is T).Cast<Control>();
        }
        


		/// <summary>
        /// Returns a collection of the sibling elements before this node, in document order
        /// which match the given type.
        /// </summary>
	    public static IEnumerable<Control> ElementsBeforeSelf<T>(this Control item)
        {
			return item.ElementsBeforeSelf().Where(i => i is T).Cast<Control>();
        }
        
        /// <summary>
        /// Returns a collection of the after elements after this node, in document order
        /// which match the given type.
        /// </summary>
	    public static IEnumerable<Control> ElementsAfterSelf<T>(this Control item)
        {
			return item.ElementsAfterSelf().Where(i => i is T).Cast<Control>();
        }

        /// <summary>
        /// Returns a collection containing this element and all descendant elements
        /// which match the given type.
        /// </summary>
	    public static IEnumerable<Control> DescendantsAndSelf<T>(this Control item)
        {
            return item.DescendantsAndSelf().Where(i => i is T).Cast<Control>();
        }
        
        /// <summary>
        /// Returns a collection of ancestor elements which match the given type.
        /// </summary>
	    public static IEnumerable<Control> Ancestors<T>(this Control item)
        {
            return item.Ancestors().Where(i => i is T).Cast<Control>();
        }
        
        /// <summary>
        /// Returns a collection containing this element and all ancestor elements
        /// which match the given type.
        /// </summary>
	    public static IEnumerable<Control> AncestorsAndSelf<T>(this Control item)
        {
            return item.AncestorsAndSelf().Where(i => i is T).Cast<Control>();
        }
        
        /// <summary>
        /// Returns a collection of child elements which match the given type.
        /// </summary>
	    public static IEnumerable<Control> Elements<T>(this Control item)
        {
            return item.Elements().Where(i => i is T).Cast<Control>();
        }
        
        /// <summary>
        /// Returns a collection containing this element and all child elements.
        /// which match the given type.
        /// </summary>
	    public static IEnumerable<Control> ElementsAndSelf<T>(this Control item)
        {
            return item.ElementsAndSelf().Where(i => i is T).Cast<Control>();
        }
        
    }
    
    public static class EnumerableTreeExtensions
    {
		/// <summary>
        /// Applies the given function to each of the items in the supplied
        /// IEnumerable.
        /// </summary>
        private static IEnumerable<Control> DrillDown(this IEnumerable<Control> items,
            Func<Control, IEnumerable<Control>> function)
        {
            foreach(var item in items)
            {
                foreach(var itemChild in function(item))
                {
                    yield return itemChild;
                }
            }
        }

       
        /// <summary>
        /// Applies the given function to each of the items in the supplied
        /// IEnumerable, which match the given type.
        /// </summary>
        public static IEnumerable<Control> DrillDown<T>(this IEnumerable<Control> items,
            Func<Control, IEnumerable<Control>> function)
            where T : Control
        {
            foreach(var item in items)
            {
                foreach(var itemChild in function(item))
                {
                    if (itemChild is T)
                    {
                        yield return (T)itemChild;
                    }
                }
            }
        }

    
        /// <summary>
        /// Returns a collection of descendant elements.
        /// </summary>
	    public static IEnumerable<Control> Descendants(this IEnumerable<Control> items)
        {
            return items.DrillDown(i => i.Descendants());
        }    
           
        /// <summary>
        /// Returns a collection containing this element and all descendant elements.
        /// </summary>
	    public static IEnumerable<Control> DescendantsAndSelf(this IEnumerable<Control> items)
        {            
            return items.DrillDown(i => i.DescendantsAndSelf());
        }
        
        /// <summary>
        /// Returns a collection of ancestor elements.
        /// </summary>
	    public static IEnumerable<Control> Ancestors(this IEnumerable<Control> items)
        {
            return items.DrillDown(i => i.Ancestors());
        } 
        
        /// <summary>
        /// Returns a collection containing this element and all ancestor elements.
        /// </summary>
	    public static IEnumerable<Control> AncestorsAndSelf(this IEnumerable<Control> items)
        {
            return items.DrillDown(i => i.AncestorsAndSelf());
        }
        
        /// <summary>
        /// Returns a collection of child elements.
        /// </summary>
	    public static IEnumerable<Control> Elements(this IEnumerable<Control> items)
        {
            return items.DrillDown(i => i.Elements());
        }
        
        /// <summary>
        /// Returns a collection containing this element and all child elements.
        /// </summary>
	    public static IEnumerable<Control> ElementsAndSelf(this IEnumerable<Control> items)
        {
            return items.DrillDown(i => i.ElementsAndSelf());
        }

       
        /// <summary>
        /// Returns a collection of descendant elements which match the given type.
        /// </summary>
	    public static IEnumerable<Control> Descendants<T>(this IEnumerable<Control> items)
	        where T : Control
        {
            return items.DrillDown<T>(i => i.Descendants());
        }
        
        /// <summary>
        /// Returns a collection containing this element and all descendant elements.
        /// which match the given type.
        /// </summary>
	    public static IEnumerable<Control> DescendantsAndSelf<T>(this IEnumerable<Control> items)
	        where T : Control
        {
            return items.DrillDown<T>(i => i.DescendantsAndSelf());
        }
        
        /// <summary>
        /// Returns a collection of ancestor elements which match the given type.
        /// </summary>
	    public static IEnumerable<Control> Ancestors<T>(this IEnumerable<Control> items)
	        where T : Control
        {
            return items.DrillDown<T>(i => i.Ancestors());
        }
        
        /// <summary>
        /// Returns a collection containing this element and all ancestor elements.
        /// which match the given type.
        /// </summary>
	    public static IEnumerable<Control> AncestorsAndSelf<T>(this IEnumerable<Control> items)
	        where T : Control
        {
            return items.DrillDown<T>(i => i.AncestorsAndSelf());
        }
        
        /// <summary>
        /// Returns a collection of child elements which match the given type.
        /// </summary>
	    public static IEnumerable<Control> Elements<T>(this IEnumerable<Control> items)
	        where T : Control
        {
            return items.DrillDown<T>(i => i.Elements());
        }
        
        /// <summary>
        /// Returns a collection containing this element and all child elements.
        /// which match the given type.
        /// </summary>
	    public static IEnumerable<Control> ElementsAndSelf<T>(this IEnumerable<Control> items)
	        where T : Control
        {
            return items.DrillDown<T>(i => i.ElementsAndSelf());
        }
    }
}