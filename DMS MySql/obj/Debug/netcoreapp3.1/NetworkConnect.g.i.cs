// Updated by XamlIntelliSenseFileGenerator 23.10.2021 21:54:58
#pragma checksum "..\..\..\NetworkConnect.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "EA6E624A851FC0D3C82DEF28D17764E71A2F6CAD"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using DMS_MySql;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace DMS_MySql
{


    /// <summary>
    /// NetworkConnect
    /// </summary>
    public partial class NetworkConnect : System.Windows.Window, System.Windows.Markup.IComponentConnector
    {


#line 32 "..\..\..\NetworkConnect.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem Menu_Item_Settings;

#line default
#line hidden


#line 33 "..\..\..\NetworkConnect.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem Menu_Item_Exit;

#line default
#line hidden


#line 37 "..\..\..\NetworkConnect.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem Menu_Item_Save;

#line default
#line hidden


#line 38 "..\..\..\NetworkConnect.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem Menu_Item_Load;

#line default
#line hidden


#line 39 "..\..\..\NetworkConnect.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem History_connection;

#line default
#line hidden


#line 53 "..\..\..\NetworkConnect.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Button_Network_Back;

#line default
#line hidden


#line 54 "..\..\..\NetworkConnect.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Button_connect_NetworkConnect;

#line default
#line hidden

        private bool _contentLoaded;

        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "5.0.1.0")]
        public void InitializeComponent()
        {
            if (_contentLoaded)
            {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/DMS MySql;component/networkconnect.xaml", System.UriKind.Relative);

#line 1 "..\..\..\NetworkConnect.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);

#line default
#line hidden
        }

        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "5.0.1.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target)
        {
            switch (connectionId)
            {
                case 1:
                    this.Menu_Item_Settings = ((System.Windows.Controls.MenuItem)(target));
                    return;
                case 2:
                    this.Menu_Item_Exit = ((System.Windows.Controls.MenuItem)(target));
                    return;
                case 3:
                    this.Menu_Item_Save = ((System.Windows.Controls.MenuItem)(target));
                    return;
                case 4:
                    this.Menu_Item_Load = ((System.Windows.Controls.MenuItem)(target));
                    return;
                case 5:
                    this.History_connection = ((System.Windows.Controls.MenuItem)(target));
                    return;
                case 6:
                    this.Button_Network_Back = ((System.Windows.Controls.Button)(target));
                    return;
                case 7:
                    this.Button_connect_NetworkConnect = ((System.Windows.Controls.Button)(target));
                    return;
            }
            this._contentLoaded = true;
        }

        internal System.Windows.Controls.TextBox Host;
        internal System.Windows.Controls.TextBox Username;
        internal System.Windows.Controls.TextBox Port;
        internal System.Windows.Controls.TextBox Password;
        internal System.Windows.Controls.TextBox Database;
    }
}

