/* ***** BEGIN LICENSE BLOCK *****
 * Version: MPL 1.1/GPL 2.0/LGPL 2.1
 *
 * The contents of this file are subject to the Mozilla Public License Version
 * 1.1 (the "License"); you may not use this file except in compliance with
 * the License. You may obtain a copy of the License at
 * http://www.mozilla.org/MPL/
 *
 * Software distributed under the License is distributed on an "AS IS" basis,
 * WITHOUT WARRANTY OF ANY KIND, either express or implied. See the License
 * for the specific language governing rights and limitations under the
 * License.
 *
 * The Original Code is the FreeCL.Net library.
 *
 * The Initial Developer of the Original Code is 
 *  Oleksii Prudkyi (Oleksii.Prudkyi@gmail.com).
 * Portions created by the Initial Developer are Copyright (C) 2005-2008
 * the Initial Developer. All Rights Reserved.
 *
 * Contributor(s):
 *
 * Alternatively, the contents of this file may be used under the terms of
 * either the GNU General Public License Version 2 or later (the "GPL"), or
 * the GNU Lesser General Public License Version 2.1 or later (the "LGPL"),
 * in which case the provisions of the GPL or the LGPL are applicable instead
 * of those above. If you wish to allow use of your version of this file only
 * under the terms of either the GPL or the LGPL, and not to allow others to
 * use your version of this file under the terms of the MPL, indicate your
 * decision by deleting the provisions above and replace them with the notice
 * and other provisions required by the GPL or the LGPL. If you do not delete
 * the provisions above, a recipient may use your version of this file under
 * the terms of any one of the MPL, the GPL or the LGPL.
 *
 * ***** END LICENSE BLOCK ***** */

// Das Bild, das mit dem Pixel-Shader 
// bearbeitet werden soll
sampler2D input : register(s0);

/// <summary>Kontrast</summary>
/// <minValue>0,005/minValue>
/// <maxValue>4</maxValue>
/// <defaultValue>1</defaultValue>
float contrast : register(C0);

/// <summary>Helligkeit</summary>
/// <minValue>-1/minValue>
/// <maxValue>1</maxValue>
/// <defaultValue>0</defaultValue>
float brightness : register(C1);

// uv ist die Pixelposition für die die Methode main
// aufgerufen wird
// Der Rückgabewert ist der Farbwert (ARGB) des Pixels 
// nach der Berabeitung mit dem Pixel-Shader
float4 main(float2 uv : TEXCOORD) : COLOR 
{
	// Den Farbwert an der Stelle uv.xy bestimmen
	float4 color = tex2D(input, uv.xy);

	// Kontrast und Helligkeit anwenden
	color.rgb = color.rgb * contrast + brightness;

	// neu berechneten Farbwert zurück geben
	return color;
}