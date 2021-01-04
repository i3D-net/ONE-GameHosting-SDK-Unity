using System;
using System.Collections;
using i3D;
using UnityEngine;

public class Test : MonoBehaviour
{
	private OneServerWrapper _server;

	IEnumerator Start ()
	{
		StartCoroutine(StatusRoutine());
		
		Debug.Log("<b>=== Created server ===</b>");
		
		yield return new WaitForSeconds(1);
		
		Debug.Log("<b>=== Sending starting ===</b>");
		_server.SetApplicationInstanceStatus(OneApplicationInstanceStatus.OneServerStarting);
		Debug.Log("<b>=== Starting sent ===</b>");
		
		yield return new WaitForSeconds(3);
		
		Debug.Log("<b>=== Sending online ===</b>");
		_server.SetApplicationInstanceStatus(OneApplicationInstanceStatus.OneServerOnline);
		Debug.Log("<b>=== Online sent ===</b>");
		
		yield return new WaitUntil(() => _server.Status == OneServerStatus.OneServerStatusReady);
		
		Debug.Log("<b>=== Creating object ===</b>");
		
		Debug.Log("<b>=== Sending live update 1 ===</b>");

		var o = CreateObject();
		
		_server.SetLiveState(1,
		                     10,
		                     "Test game ЫЫЫ",
		                     "Test map",
		                     "Test mode",
		                     "v0.1.0.1",
		                     o);
		
		o.Dispose();
		
		Debug.Log("<b>=== Live update 1 sent ===</b>");
		
		yield return new WaitForSeconds(5);
		
		Debug.Log("<b>=== Sending live update 2 ===</b>");
		
		_server.SetLiveState(3,
		                     10,
		                     "Test game 2 Œ",
		                     "Test Œ map 2",
		                     "Test mode 2",
		                     "v0.1.0.2",
		                     null);
		
		Debug.Log("<b>=== Live update 2 sent ===</b>");
		
		yield return new WaitForSeconds(5);
		
		Debug.Log("<b>=== Sending live update 3 ===</b>");
		
		_server.SetLiveState(7,
		                     10,
		                     "Test game 3",
		                     "Test map 3",
		                     "Test mode 3",
		                     "v0.1.0.3",
		                     null);
		
		Debug.Log("<b>=== Live update 3 sent ===</b>");
	}

	private static OneObject CreateObject()
	{
		var arr = new OneArray(3);
		arr.PushBool(true);
		arr.PushInt(456);
		arr.PushString("qwe ЫЫЫ");
		
		var o = new OneObject();
		o.SetInt("q", 23);
		o.SetInt("ww", 777);
		o.SetString("r", "rrr");
		o.SetString("Œ", "Te Œ Te");
		o.SetArray("arr", arr);

		var obj = new OneObject();
		obj.SetBool("A", true);
		obj.SetBool("B", false);
		obj.SetInt("C", 123);
		obj.SetString("D", "ddd");
		obj.SetArray("E", arr);
		obj.SetObject("O", o);

		Debug.Log(obj.GetObject("O").GetString("Œ"));
		Debug.Log(obj.GetObject("O").GetString("r"));
		Debug.Log(obj.GetObject("O").GetArray("arr").GetInt(1));
		Debug.Log(obj.GetObject("O").GetArray("arr").GetString(2));

		return o;
	}

	private void ServerOnApplicationInstanceInformationReceived(OneObject obj)
	{
		Debug.Log("ServerOnApplicationInstanceInformationReceived");
	}

	private void ServerOnSoftStopReceived(int timeout)
	{
		Debug.Log("ServerOnSoftStopReceived: " + timeout);
	}

	private void ServerOnHostInformationReceived(OneObject obj)
	{
		Debug.Log("ServerOnHostInformationReceived");
	}

	private void ServerOnMetadataReceived(OneArray obj)
	{
		Debug.Log("ServerOnMetadataReceived");
	}

	private void ServerOnAllocatedReceived(OneArray obj)
	{
		Debug.Log("ServerOnAllocatedReceived");
	}

	private void Update()
	{
		if (_server == null)
			throw new InvalidOperationException("Server is null");

		_server.Update();
	}

	IEnumerator StatusRoutine()
	{
		Debug.Log("=== Status getting routine start");

		while (true)
		{
			yield return new WaitForSeconds(5);

			Debug.Log("===== Getting status");
			Debug.Log(_server.Status.ToString());
		}
	}

	private void OnEnable()
	{
		if (_server != null)
			throw new InvalidOperationException("Server should be null");

		Debug.Log("<b>=== Creating the server ===</b>");

		_server = new OneServerWrapper(null,//(l, s) => Debug.LogFormat("{0}: {1}", l.ToString(), s),
		                        19001);
		
		_server.AllocatedReceived += ServerOnAllocatedReceived;
		_server.MetadataReceived += ServerOnMetadataReceived;
		_server.HostInformationReceived += ServerOnHostInformationReceived;
		_server.SoftStopReceived += ServerOnSoftStopReceived;
		_server.ApplicationInstanceInformationReceived += ServerOnApplicationInstanceInformationReceived;
	}

	private void OnDisable()
	{
		if (_server != null)
		{
			_server.Dispose() ;
			_server = null;
		}
	}
}
