﻿// Copyright 2013-2021 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
//
// Licensed under the ImageMagick License (the "License"); you may not use this file except in
// compliance with the License. You may obtain a copy of the License at
//
//   https://imagemagick.org/script/license.php
//
// Unless required by applicable law or agreed to in writing, software distributed under the
// License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
// either express or implied. See the License for the specific language governing permissions
// and limitations under the License.

#if Q8
using QuantumType = System.Byte;
#elif Q16
using QuantumType = System.UInt16;
#elif Q16HDRI
using QuantumType = System.Single;
#else
#error Not implemented!
#endif

namespace ImageMagick
{
    internal static class IMorphologySettingsExtensions
    {
        public static void SetImageArtifacts(this IMorphologySettings self, IMagickImage<QuantumType> image)
        {
            if (self.ConvolveBias != null)
                image.SetArtifact("convolve:bias", self.ConvolveBias.ToString());

            if (self.ConvolveScale != null)
                image.SetArtifact("convolve:scale", self.ConvolveScale.ToString());
        }

        public static void RemoveImageArtifacts(this IMorphologySettings self, IMagickImage<QuantumType> image)
        {
            if (self.ConvolveBias != null)
                image.RemoveArtifact("convolve:bias");

            if (self.ConvolveScale != null)
                image.RemoveArtifact("convolve:scale");
        }
    }
}
