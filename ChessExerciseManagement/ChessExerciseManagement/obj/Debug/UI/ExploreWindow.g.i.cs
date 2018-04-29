﻿#pragma checksum "..\..\..\UI\ExploreWindow.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "4DF199EA293486B6F03450B834580853DB12C4F0"
//------------------------------------------------------------------------------
// <auto-generated>
//     Dieser Code wurde von einem Tool generiert.
//     Laufzeitversion:4.0.30319.42000
//
//     Änderungen an dieser Datei können falsches Verhalten verursachen und gehen verloren, wenn
//     der Code erneut generiert wird.
// </auto-generated>
//------------------------------------------------------------------------------

using ChessExerciseManagement.UI;
using ChessExerciseManagement.UI.UserControls;
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


namespace ChessExerciseManagement.UI {
    
    
    /// <summary>
    /// ExploreWindow
    /// </summary>
    public partial class ExploreWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 12 "..\..\..\UI\ExploreWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button SearchButton;
        
        #line default
        #line hidden
        
        
        #line 13 "..\..\..\UI\ExploreWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox UsedkeywordTextBox;
        
        #line default
        #line hidden
        
        
        #line 14 "..\..\..\UI\ExploreWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label UsedKeywordText;
        
        #line default
        #line hidden
        
        
        #line 16 "..\..\..\UI\ExploreWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox KeywordTextBox;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\..\UI\ExploreWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox ExerciseListBox;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\..\UI\ExploreWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ExportButton;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\..\UI\ExploreWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal ChessExerciseManagement.UI.UserControls.BoardControl Boardcontrol;
        
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
            System.Uri resourceLocater = new System.Uri("/ChessExerciseManagement;component/ui/explorewindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\UI\ExploreWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal System.Delegate _CreateDelegate(System.Type delegateType, string handler) {
            return System.Delegate.CreateDelegate(delegateType, this, handler);
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
            
            #line 9 "..\..\..\UI\ExploreWindow.xaml"
            ((ChessExerciseManagement.UI.ExploreWindow)(target)).Loaded += new System.Windows.RoutedEventHandler(this.Window_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.SearchButton = ((System.Windows.Controls.Button)(target));
            
            #line 12 "..\..\..\UI\ExploreWindow.xaml"
            this.SearchButton.Click += new System.Windows.RoutedEventHandler(this.SearchButton_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.UsedkeywordTextBox = ((System.Windows.Controls.TextBox)(target));
            
            #line 13 "..\..\..\UI\ExploreWindow.xaml"
            this.UsedkeywordTextBox.MouseDoubleClick += new System.Windows.Input.MouseButtonEventHandler(this.UsedkeywordTextBox_MouseDoubleClick);
            
            #line default
            #line hidden
            return;
            case 4:
            this.UsedKeywordText = ((System.Windows.Controls.Label)(target));
            return;
            case 5:
            this.KeywordTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 6:
            this.ExerciseListBox = ((System.Windows.Controls.ListBox)(target));
            
            #line 17 "..\..\..\UI\ExploreWindow.xaml"
            this.ExerciseListBox.MouseDoubleClick += new System.Windows.Input.MouseButtonEventHandler(this.ExerciseListBox_MouseDoubleClick);
            
            #line default
            #line hidden
            return;
            case 7:
            this.ExportButton = ((System.Windows.Controls.Button)(target));
            
            #line 18 "..\..\..\UI\ExploreWindow.xaml"
            this.ExportButton.Click += new System.Windows.RoutedEventHandler(this.ExportButton_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.Boardcontrol = ((ChessExerciseManagement.UI.UserControls.BoardControl)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

