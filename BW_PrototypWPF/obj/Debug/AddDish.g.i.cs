﻿#pragma checksum "..\..\AddDish.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "6E5411CA4FC1AD840F3AE2DFDA89ADFA"
//------------------------------------------------------------------------------
// <auto-generated>
//     Ten kod został wygenerowany przez narzędzie.
//     Wersja wykonawcza:4.0.30319.42000
//
//     Zmiany w tym pliku mogą spowodować nieprawidłowe zachowanie i zostaną utracone, jeśli
//     kod zostanie ponownie wygenerowany.
// </auto-generated>
//------------------------------------------------------------------------------

using BW_PrototypWPF;
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


namespace BW_PrototypWPF {
    
    
    /// <summary>
    /// AddDish
    /// </summary>
    public partial class AddDish : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 33 "..\..\AddDish.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btn_Add;
        
        #line default
        #line hidden
        
        
        #line 39 "..\..\AddDish.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btn_AddIng;
        
        #line default
        #line hidden
        
        
        #line 45 "..\..\AddDish.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btn_Close;
        
        #line default
        #line hidden
        
        
        #line 55 "..\..\AddDish.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tb_Name;
        
        #line default
        #line hidden
        
        
        #line 57 "..\..\AddDish.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tb_Time;
        
        #line default
        #line hidden
        
        
        #line 59 "..\..\AddDish.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tb_Amount;
        
        #line default
        #line hidden
        
        
        #line 60 "..\..\AddDish.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView lv_Ingredients;
        
        #line default
        #line hidden
        
        
        #line 67 "..\..\AddDish.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btn_AddIngToDish;
        
        #line default
        #line hidden
        
        
        #line 68 "..\..\AddDish.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btn_RemoveIngToDish;
        
        #line default
        #line hidden
        
        
        #line 69 "..\..\AddDish.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView lv_ToDish;
        
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
            System.Uri resourceLocater = new System.Uri("/BW_PrototypWPF;component/adddish.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\AddDish.xaml"
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
            this.btn_Add = ((System.Windows.Controls.Button)(target));
            
            #line 38 "..\..\AddDish.xaml"
            this.btn_Add.Click += new System.Windows.RoutedEventHandler(this.btn_Add_Click);
            
            #line default
            #line hidden
            return;
            case 2:
            this.btn_AddIng = ((System.Windows.Controls.Button)(target));
            
            #line 44 "..\..\AddDish.xaml"
            this.btn_AddIng.Click += new System.Windows.RoutedEventHandler(this.btn_AddIng_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.btn_Close = ((System.Windows.Controls.Button)(target));
            
            #line 50 "..\..\AddDish.xaml"
            this.btn_Close.Click += new System.Windows.RoutedEventHandler(this.btn_Close_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.tb_Name = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.tb_Time = ((System.Windows.Controls.TextBox)(target));
            return;
            case 6:
            this.tb_Amount = ((System.Windows.Controls.TextBox)(target));
            return;
            case 7:
            this.lv_Ingredients = ((System.Windows.Controls.ListView)(target));
            return;
            case 8:
            this.btn_AddIngToDish = ((System.Windows.Controls.Button)(target));
            
            #line 67 "..\..\AddDish.xaml"
            this.btn_AddIngToDish.Click += new System.Windows.RoutedEventHandler(this.btn_AddIngToDish_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            this.btn_RemoveIngToDish = ((System.Windows.Controls.Button)(target));
            
            #line 68 "..\..\AddDish.xaml"
            this.btn_RemoveIngToDish.Click += new System.Windows.RoutedEventHandler(this.btn_RemoveIngToDish_Click);
            
            #line default
            #line hidden
            return;
            case 10:
            this.lv_ToDish = ((System.Windows.Controls.ListView)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

