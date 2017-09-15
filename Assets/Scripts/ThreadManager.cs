using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

/**
 * 
 * Make a class for the Thread, this class needs a public member ThreadBase.
 * This is where you add the MAIN thread function and other helper functions and such.
 * 
 * Class that will start/own this thread needs a member of that Class above.
 * 
 * To start the MAIN function of the thread: ThreadManager.Instance.AddThread(MAIN());
 * 
 * WARNING: while(true) may crash on quit/destroy.
 * WARNING: does not work (well) in editor.
 * NOTICE: Cannot edit unity's compondents
 * 
 **/

/// <summary>
/// Base Thread
/// </summary>
public class ThreadBase
{
	ThreadManager _manager;

	public delegate void DelType();
	public volatile DelType toDo;
	public volatile bool shouldContinue = true;

	public void ThreadFunction()
	{
		while(shouldContinue)
			toDo();
	}
}

/// <summary>
/// Thread Manager
/// </summary>
public class ThreadManager : MonoBehaviour {

	private static ThreadManager instance;
	public static ThreadManager Instance
	{
		get
		{
			if(!instance)
			{
				instance = (new GameObject()).AddComponent<ThreadManager>();
				instance.name = "Thread Manager";
				DontDestroyOnLoad(instance.gameObject);
			}
			return instance;
		}
	}

	private LinkedList<ThreadBase> threads = new LinkedList<ThreadBase>();

    /// <summary>
    /// Add this thread
    /// </summary>
    /// <param name="item">thread to add</param>
    /// <returns></returns>
	public ThreadBase AddThread(ThreadBase.DelType item)
	{
		ThreadBase newT = new ThreadBase();
		newT.shouldContinue = true;
		newT.toDo = item;
		new Thread(newT.ThreadFunction).Start();

		threads.AddLast(newT);

		return newT;
	}

    /// <summary>
    /// Remove this tread
    /// </summary>
    /// <param name="toRemove">thread to remove</param>
	public void RemoveThread(ThreadBase toRemove)
	{
		if(threads.Remove(toRemove))
		{
			toRemove.shouldContinue = false;
		}
	}

    /// <summary>
    /// When destroyed, end all threads.
    /// </summary>
	void OnDestroy()
	{
		Debug.Log("Destroy Thread Manager");
		for(LinkedListNode<ThreadBase> node = threads.First; node != null; node = node.Next)
		{
			node.Value.shouldContinue = false;
		}
	}

}
