﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Dieser Code wurde von einem Tool generiert.
//     Laufzeitversion:2.0.50727.6400
//
//     Änderungen an dieser Datei können falsches Verhalten verursachen und gehen verloren, wenn
//     der Code erneut generiert wird.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// Dieser Quellcode wurde automatisch generiert von xsd, Version=2.0.50727.3038.
// 
namespace AP_HA {
    using System.Xml.Serialization;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(TypeName="HausarbeitAP.ProjectCT", Namespace="urn:HausarbeitAP")]
    [System.Xml.Serialization.XmlRootAttribute("project", Namespace="urn:HausarbeitAP", IsNullable=false)]
    public partial class HausarbeitAPProjectCT : object, System.ComponentModel.INotifyPropertyChanged {
        
        private HausarbeitAPLabelCT[] labelsField;
        
        private HausarbeitAPSectionCT sectionField;
        
        private string nameField;
        
        private string descriptionField;
        
        private int totalLayersField;
        
        private int widthField;
        
        private int heightField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("label", IsNullable=false)]
        public HausarbeitAPLabelCT[] labels {
            get {
                return this.labelsField;
            }
            set {
                this.labelsField = value;
                this.RaisePropertyChanged("labels");
            }
        }
        
        /// <remarks/>
        public HausarbeitAPSectionCT section {
            get {
                return this.sectionField;
            }
            set {
                this.sectionField = value;
                this.RaisePropertyChanged("section");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string name {
            get {
                return this.nameField;
            }
            set {
                this.nameField = value;
                this.RaisePropertyChanged("name");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string description {
            get {
                return this.descriptionField;
            }
            set {
                this.descriptionField = value;
                this.RaisePropertyChanged("description");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public int totalLayers {
            get {
                return this.totalLayersField;
            }
            set {
                this.totalLayersField = value;
                this.RaisePropertyChanged("totalLayers");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public int width {
            get {
                return this.widthField;
            }
            set {
                this.widthField = value;
                this.RaisePropertyChanged("width");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public int height {
            get {
                return this.heightField;
            }
            set {
                this.heightField = value;
                this.RaisePropertyChanged("height");
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(TypeName="HausarbeitAP.LabelCT", Namespace="urn:HausarbeitAP")]
    public partial class HausarbeitAPLabelCT : object, System.ComponentModel.INotifyPropertyChanged {
        
        private int idField;
        
        private string titleField;
        
        private string descriptionField;
        
        private int colorField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public int id 
        {
            get 
            {
                return this.idField;
            }
            set 
            {
                this.idField = value;
                this.RaisePropertyChanged("id");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string title 
        {
            get 
            {
                return this.titleField;
            }
            set 
            {
                this.titleField = value;
                this.RaisePropertyChanged("title");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string description 
        {
            get 
            {
                return this.descriptionField;
            }
            set 
            {
                this.descriptionField = value;
                this.RaisePropertyChanged("description");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public int color 
        {
            get 
            {
                return this.colorField;
            }
            set 
            {
                this.colorField = value;
                this.RaisePropertyChanged("color");
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) 
        {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) 
            {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(TypeName="HausarbeitAP.SectionCT", Namespace="urn:HausarbeitAP")]
    public partial class HausarbeitAPSectionCT : object, System.ComponentModel.INotifyPropertyChanged 
    {
        
        private int xField;
        
        private int yField;
        
        private int widthField;
        
        private int heightField;
        
        private int zField;
        
        private int depthField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public int x {
            get {
                return this.xField;
            }
            set {
                this.xField = value;
                this.RaisePropertyChanged("x");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public int y 
        {
            get 
            {
                return this.yField;
            }
            set 
            {
                this.yField = value;
                this.RaisePropertyChanged("y");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public int width 
        {
            get 
            {
                return this.widthField;
            }
            set 
            {
                this.widthField = value;
                this.RaisePropertyChanged("width");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public int height 
        {
            get 
            {
                return this.heightField;
            }
            set {
                this.heightField = value;
                this.RaisePropertyChanged("height");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public int z 
        {
            get 
            {
                return this.zField;
            }
            set {
                this.zField = value;
                this.RaisePropertyChanged("z");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public int depth 
        {
            get 
            {
                return this.depthField;
            }
            set 
            {
                this.depthField = value;
                this.RaisePropertyChanged("depth");
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) 
        {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) 
            {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
