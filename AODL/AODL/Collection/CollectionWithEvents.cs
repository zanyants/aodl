/*
 * $Id: CollectionWithEvents.cs,v 1.2 2005/10/08 08:19:25 larsbm Exp $
 */

using System.Collections;

namespace AODL.Collections
{
	public class CollectionWithEvents : CollectionBase
	{
	   // Declare the event signatures
	   public delegate void CollectionClear();
	   public delegate void CollectionChange(int index, object value);

	   // Collection change events
	   public event CollectionClear Clearing;
	   public event CollectionClear Cleared;
	   public event CollectionChange Inserting;
	   public event CollectionChange Inserted;
	   public event CollectionChange Removing;
	   public event CollectionChange Removed;

	   // Overrides for generating events
	   protected override void OnClear() 
	   { 
	      if (Clearing != null) Clearing(); 
	   }		

	   protected override void OnClearComplete() 
	   { 
	      if (Cleared != null) Cleared(); 
	   }	

	   protected override void OnInsert(int index, object value) 
	   {
	      if (Inserting != null) Inserting(index, value);
	   }

	   protected override void OnInsertComplete(int index, object value)
	   {
	      if (Inserted != null) Inserted(index, value);
	   }

	   protected override void OnRemove(int index, object value)
	   {
	      if (Removing != null) Removing(index, value);
	   }

	   protected override void OnRemoveComplete(int index, object value)
	   {
	      if (Removed != null) Removed(index, value);
	   }
	}

}

/*
 * $Log: CollectionWithEvents.cs,v $
 * Revision 1.2  2005/10/08 08:19:25  larsbm
 * - added cvs tags
 *
 */