using Rhino;
using Rhino.PlugIns;
using System;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace HornbillRhinoPlugin
{
    ///<summary>
    /// <para>Every RhinoCommon .rhp assembly must have one and only one PlugIn-derived
    /// class. DO NOT create instances of this class yourself. It is the
    /// responsibility of Rhino to create an instance of this class.</para>
    /// <para>To complete plug-in information, please also see all PlugInDescription
    /// attributes in AssemblyInfo.cs (you might need to click "Project" ->
    /// "Show All Files" to see it in the "Solution Explorer" window).</para>
    ///</summary>
    public class HornbillRhinoPlugin : FileImportPlugIn
    {
        public HornbillRhinoPlugin()
        {
            Instance = this;
        }

        ///<summary>Gets the only instance of the HornbillRhinoPlugin plug-in.</summary>
        public static HornbillRhinoPlugin Instance { get; private set; }

        public override PlugInLoadTime LoadTime => PlugInLoadTime.AtStartup;

        ///<summary>Defines file extensions that this import plug-in is designed to read.</summary>
        /// <param name="options">Options that specify how to read files.</param>
        /// <returns>A list of file types that can be imported.</returns>
        protected override FileTypeList AddFileTypes(Rhino.FileIO.FileReadOptions options)
        {
            var result = new FileTypeList();
            result.AddFileType("Javascript File (*.js)", "js");
            return result;
        }

        /// <summary>
        /// Is called when a user requests to import a .js file.
        /// It is actually up to this method to read the file itself.
        /// </summary>
        /// <param name="filename">The complete path to the new file.</param>
        /// <param name="index">The index of the file type as it had been specified by the AddFileTypes method.</param>
        /// <param name="doc">The document to be written.</param>
        /// <param name="options">Options that specify how to write file.</param>
        /// <returns>A value that defines success or a specific failure.</returns>
        protected override bool ReadFile(string filename, int index, RhinoDoc doc, Rhino.FileIO.FileReadOptions options)
        {
            bool read_success = false;
            // TODO: Add code for reading file
            return read_success;
        }
        // You can override methods here to change the plug-in behavior on
        // loading and shut down, add options pages to the Rhino _Option command
        // and maintain plug-in wide options in a document.

        internal WebSocketServer ws;

        protected override LoadReturnCode OnLoad(ref string errorMessage)
        {
            try
            {
                ws = new WebSocketServer(9393);
                ws.AddWebSocketService<Listener>("/");
                ws.Start();
            }
            catch (Exception ex)
            {
                RhinoApp.WriteLine(ex.Message);
                RhinoApp.WriteLine("Start javascript host failed!");
            }
            return base.OnLoad(ref errorMessage);
        }

        protected override void OnShutdown()
        {
            ws.Stop();
            base.OnShutdown();
        }

        private class Listener : WebSocketBehavior
        {
            protected override void OnMessage(MessageEventArgs e)
            {
                try
                {
                    var module = new RhinoHornbill.JavascriptHost(e.Data);
                    module.Run();
                }
                catch (Exception ex)
                {
                    RhinoApp.WriteLine(ex.Message);
                }
            }
        }
    }
}