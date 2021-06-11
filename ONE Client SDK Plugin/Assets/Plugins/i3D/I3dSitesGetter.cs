using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace i3D
{
    /// <summary>
    /// The main object of the ONE SDK. A single server should be created per game server.
    /// The server needs to be updated often to send and receive messages from an Arcus Client.
    /// </summary>
    public class I3dSitesGetter : MonoBehaviour
    {
        /// <summary>
        /// Returns the status of the SitesGetter.
        /// </summary>
        public I3dSitesGetterStatus Status
        {
            get
            {
                return _wrapper.Status;
            }
        }

        public I3dSitesGetterWrapper Wrapper
        {
            get
            {
                return _wrapper;
            }
        }

        /// <summary>
        /// Sets the IPv4 List of the SitesGetter on list.
        /// </summary>
        public void ipv4List(I3dIpList list)
        {
            _wrapper.ipv4List(list);
        }

        public void Update()
        {
            if (_wrapper == null)
                throw new InvalidOperationException("I3d SitesGetterWrapper is null");

            _wrapper.Update();
        }

        private void OnEnable()
        {
            _wrapper = new I3dSitesGetterWrapper();
            _ptr = _wrapper.Ptr;
            _wrapper.Init(null, HttpCallback, _ptr);

            lock (SitesGetter)
            {
                SitesGetter.Add(_ptr, this);
            }
        }

        private void OnDisable()
        {
            if (_wrapper != null)
            {
                _wrapper.Dispose();
                _wrapper = null;
            }

            lock (SitesGetter)
            {
                SitesGetter.Remove(_ptr);
            }
        }

        private static void HttpCallback(IntPtr url, I3dSitesGetterWrapper.I3dHttpParsingCallback parsingCallback, IntPtr parsingUserdata, IntPtr userdata) {
            I3dSitesGetter sitesGetter;

            if (!SitesGetter.TryGetValue(userdata, out sitesGetter))
                throw new InvalidOperationException("Cannot find I3dSitesGetter instance");

            sitesGetter.StartCoroutine(sitesGetter.GetPayload(url, parsingCallback, parsingUserdata, userdata));
        }

        IEnumerator GetPayload(IntPtr url, I3dSitesGetterWrapper.I3dHttpParsingCallback parsingCallback, IntPtr parsingUserdata, IntPtr userdata) {
            using(var url_string = new Utf8ByteArray(url)) {
                UnityWebRequest www = UnityWebRequest.Get(url_string.ToString());
                yield return www.SendWebRequest();

                using(var json = new Utf8ByteArray(www.downloadHandler.text))
                {
                    parsingCallback(true, json, parsingUserdata);
                }
            }
        }

        private IntPtr _ptr;
        private I3dSitesGetterWrapper _wrapper;

        private static readonly Dictionary<IntPtr, I3dSitesGetter> SitesGetter = new Dictionary<IntPtr, I3dSitesGetter>();

    }
}