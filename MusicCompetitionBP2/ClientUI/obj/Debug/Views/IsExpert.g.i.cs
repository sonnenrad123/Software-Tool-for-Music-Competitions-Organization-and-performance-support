// Updated by XamlIntelliSenseFileGenerator 29.5.2021. 16:38:13
#pragma checksum "..\..\..\Views\IsExpert.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "60EFC1F6577443B60EA056DF8A03F6CE0E338E5B9CEB76421D23232BB2E19068"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using ClientUI.Views;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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


namespace ClientUI.Views
{


    /// <summary>
    /// IsExpert
    /// </summary>
    public partial class IsExpert : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector
    {

#line default
#line hidden

        private bool _contentLoaded;

        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent()
        {
            if (_contentLoaded)
            {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/ClientUI;component/views/isexpert.xaml", System.UriKind.Relative);

#line 1 "..\..\..\Views\IsExpert.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);

#line default
#line hidden
        }

        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target)
        {
            switch (connectionId)
            {
                case 1:
                    this.tabela = ((System.Windows.Controls.DataGrid)(target));
                    return;
                case 2:
                    this.GenreCB = ((System.Windows.Controls.ComboBox)(target));
                    return;
                case 3:
                    this.CompetitionCB = ((System.Windows.Controls.ComboBox)(target));
                    return;
                case 4:
                    this.AddBtn = ((System.Windows.Controls.Button)(target));
                    return;
                case 5:
                    this.DeleteBtn = ((System.Windows.Controls.Button)(target));
                    return;
            }
            this._contentLoaded = true;
        }
    }
}

