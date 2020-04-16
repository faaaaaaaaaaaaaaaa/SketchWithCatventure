using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCollider : MonoBehaviour
{
    static TestCollider lastDragNode;
    static List<TestCollider> draggedNodes = new List<TestCollider>();
    int count = 0;
    int dragCount = 0;
    int diff = 0;
    int obCount = 0;
    int sumDrag = 0;

    public int nodeType = 0;

    private void Start()
    {
        count = GameObject.FindGameObjectsWithTag("CheckPoint").Length;
        
        //Debug.Log("Count GameObject = " + count);
        //CheckPoint();
    }

    private void OnMouseDrag()
    {
        if (lastDragNode == null)
        {
            draggedNodes.Add(this);
            lastDragNode = this;
        }
    }

        

    private void OnMouseOver()
    {
        
        if (lastDragNode == null || lastDragNode == this)
            return;

        var tempLastDragNodeLayer = lastDragNode.gameObject.layer;
        lastDragNode.gameObject.layer = 2; // Set layer to ignore raycast
        var result = Physics2D.Linecast(lastDragNode.transform.position, transform.position);
        if (lastDragNode.nodeType == nodeType &&
            result.collider.gameObject == gameObject /* Check current node is not obstructed by other node types */)
        {
            lastDragNode.gameObject.layer = tempLastDragNodeLayer;
            draggedNodes.Add(this);
            lastDragNode = this;
        }
        else
        {
            lastDragNode.gameObject.layer = tempLastDragNodeLayer;
        }
    }

    private void OnMouseUp()
    {
        if (draggedNodes.Count > 1)
        {
            // Collect score
            int score = draggedNodes.Count ;
            Debug.Log("Count Node = " + score + " nodeType = " + lastDragNode.nodeType);
            // Clear all dragged nodes
            for (int i = draggedNodes.Count - 1; i >= 0; --i)
            {
                Destroy(draggedNodes[i].gameObject);
            }
            //CheckPoint(score);
            
        }
        
        dis();
        // Debug.Log("------------------------------------------" + draggedNodes.Count);
        draggedNodes.Clear();
        lastDragNode = null;
    }
   
    /*protected void CheckPoint(int drag)
    {
       // obCount = count;
        
        Debug.Log("--------------(" + (count) + ")------------  ob");
        Debug.Log("--------------(" + (drag) + ")------------    drag");
        Debug.Log("--------------(" + (sumDrag) +"  +  " + (drag) + ")------------    sumDrag + drag");
        sumDrag = sumDrag + drag;
        Debug.Log("--------------(" + (sumDrag) + ")------------    sumDrag");
        
        // dragCount += dragCount;
        diff = (count - forReturn());
        Debug.Log("--------------(" + (diff) + ")------------   diff");
 
    }

    int summ = 0;
    protected int forReturn()
    {
        summ += sumDrag;
        Debug.Log("--------------(" + (summ) + ")------------   summ---forreturn()");
        return summ;
    }*/

    private void dis()
    {
        sumDrag = draggedNodes.Count;
        diff = sumDrag + diff;
        Debug.Log("--------------(" + (sumDrag) + ")------------    sumDrag " + "---- dragNode.Count ---- " + draggedNodes.Count);
        Debug.Log("--------------(" + (diff) + ")------------    diff ");
        
    }
}
