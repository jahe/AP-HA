using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Media3D;

namespace AP_HA
{
    public class FirstEffect : ShaderEffect
    {
        public static readonly DependencyProperty InputProperty = ShaderEffect.RegisterPixelShaderSamplerProperty("Input", typeof(FirstEffect), 0);
        public static readonly DependencyProperty ContrastProperty = DependencyProperty.Register("Contrast", typeof(double), typeof(FirstEffect), new UIPropertyMetadata(((double)(1D)), PixelShaderConstantCallback(0)));
        public static readonly DependencyProperty BrightnessProperty = DependencyProperty.Register("Brightness", typeof(double), typeof(FirstEffect), new UIPropertyMetadata(((double)(0D)), PixelShaderConstantCallback(1)));
        
        public FirstEffect()
        {
            PixelShader pixelShader = new PixelShader();
            pixelShader.UriSource = new Uri("/AP-HA;component/PixelShader/FirstEffect.ps", UriKind.Relative);
            this.PixelShader = pixelShader;

            this.UpdateShaderValue(InputProperty);
            this.UpdateShaderValue(ContrastProperty);
            this.UpdateShaderValue(BrightnessProperty);
        }

        public Brush Input
        {
            get
            {
                return ((Brush)(this.GetValue(InputProperty)));
            }
            set
            {
                this.SetValue(InputProperty, value);
            }
        }

        public double Contrast
        {
            get
            {
                return ((double)(this.GetValue(ContrastProperty)));
            }
            set
            {
                this.SetValue(ContrastProperty, value);
            }
        }

        public double Brightness
        {
            get
            {
                return ((double)(this.GetValue(BrightnessProperty)));
            }
            set
            {
                this.SetValue(BrightnessProperty, value);
            }
        }
    }
}
