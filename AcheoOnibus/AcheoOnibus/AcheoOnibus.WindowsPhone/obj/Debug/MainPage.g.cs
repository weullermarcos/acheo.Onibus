﻿

#pragma checksum "W:\SENAI - S2B\Projeto Final\acheo.Onibus\AcheoOnibus\AcheoOnibus\AcheoOnibus.WindowsPhone\MainPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "EB762460C21772D6D987224B79E34777"
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
    partial class MainPage : global::Windows.UI.Xaml.Controls.Page, global::Windows.UI.Xaml.Markup.IComponentConnector
    {
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
 
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 1:
                #line 16 "..\..\MainPage.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.btnSend_Click_1;
                 #line default
                 #line hidden
                break;
            case 2:
                #line 17 "..\..\MainPage.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.Selector)(target)).SelectionChanged += this.cmbSelection_SelectionChanged;
                 #line default
                 #line hidden
                break;
            case 3:
                #line 18 "..\..\MainPage.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.Selector)(target)).SelectionChanged += this.cmbSentidoViagem_SelectionChanged;
                 #line default
                 #line hidden
                break;
            }
            this._contentLoaded = true;
        }
    }
}


