﻿

#pragma checksum "C:\Users\weuller.marcos\Desktop\acheo.Onibus\AcheoOnibus\AcheoOnibus\AcheoOnibus.WindowsPhone\MapPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "38E82C76A6DBD9E19AD5CB31AA709814"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AcheoOnibus
{
    partial class MapPage : global::Windows.UI.Xaml.Controls.Page, global::Windows.UI.Xaml.Markup.IComponentConnector
    {
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
 
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 1:
                #line 14 "..\..\MapPage.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.RangeBase)(target)).ValueChanged += this.sldEixoY_ValueChanged;
                 #line default
                 #line hidden
                break;
            case 2:
                #line 15 "..\..\MapPage.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.RangeBase)(target)).ValueChanged += this.sldEixoX_ValueChanged;
                 #line default
                 #line hidden
                break;
            case 3:
                #line 16 "..\..\MapPage.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.RangeBase)(target)).ValueChanged += this.sldZoom_ValueChanged;
                 #line default
                 #line hidden
                break;
            }
            this._contentLoaded = true;
        }
    }
}


