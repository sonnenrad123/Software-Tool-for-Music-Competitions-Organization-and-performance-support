﻿#pragma checksum "..\..\..\Views\EventOrganizerTableView.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "1E2425D13E4F4F72C3EBCC54E8EAB71232AA073A7D979CE2496FE331D5AE7FDF"
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


namespace ClientUI.Views {
    
    
    /// <summary>
    /// EventOrganizerTableView
    /// </summary>
    public partial class EventOrganizerTableView : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 22 "..\..\..\Views\EventOrganizerTableView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid tabela;
        
        #line default
        #line hidden
        
        
        #line 63 "..\..\..\Views\EventOrganizerTableView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox CityCB;
        
        #line default
        #line hidden
        
        
        #line 66 "..\..\..\Views\EventOrganizerTableView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox PhCB;
        
        #line default
        #line hidden
        
        
        #line 69 "..\..\..\Views\EventOrganizerTableView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button AddBtn;
        
        #line default
        #line hidden
        
        
        #line 70 "..\..\..\Views\EventOrganizerTableView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ModifyBtn;
        
        #line default
        #line hidden
        
        
        #line 71 "..\..\..\Views\EventOrganizerTableView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button DeleteBtn;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/ClientUI;component/views/eventorganizertableview.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Views\EventOrganizerTableView.xaml"
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
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.tabela = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 2:
            
            #line 50 "..\..\..\Views\EventOrganizerTableView.xaml"
            ((System.Windows.Controls.TextBox)(target)).PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(this.PreviewTextInput);
            
            #line default
            #line hidden
            return;
            case 3:
            
            #line 62 "..\..\..\Views\EventOrganizerTableView.xaml"
            ((System.Windows.Controls.TextBox)(target)).PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(this.PreviewTextInput);
            
            #line default
            #line hidden
            return;
            case 4:
            this.CityCB = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 5:
            
            #line 65 "..\..\..\Views\EventOrganizerTableView.xaml"
            ((System.Windows.Controls.TextBox)(target)).PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(this.PreviewTextInput);
            
            #line default
            #line hidden
            return;
            case 6:
            this.PhCB = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 7:
            this.AddBtn = ((System.Windows.Controls.Button)(target));
            return;
            case 8:
            this.ModifyBtn = ((System.Windows.Controls.Button)(target));
            return;
            case 9:
            this.DeleteBtn = ((System.Windows.Controls.Button)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

