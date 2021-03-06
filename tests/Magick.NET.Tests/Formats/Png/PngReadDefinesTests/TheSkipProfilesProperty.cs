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

using ImageMagick;
using ImageMagick.Formats;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class PngReadDefinesTests
    {
        public class TheSkipProfilesProperty
        {
            [Fact]
            public void ShouldNotSetDefineWhenValueIsInvalid()
            {
                using (var image = new MagickImage())
                {
                    image.Settings.SetDefines(new PngReadDefines
                    {
                        SkipProfiles = (PngProfileTypes)64,
                    });

                    Assert.Null(image.Settings.GetDefine("profile:skip"));
                }
            }

            [Fact]
            public void ShouldSetTheDefine()
            {
                using (var image = new MagickImage())
                {
                    image.Settings.SetDefines(new PngReadDefines
                    {
                        SkipProfiles = PngProfileTypes.Icc | PngProfileTypes.Iptc,
                    });

                    Assert.Equal("Icc,Iptc", image.Settings.GetDefine("profile:skip"));
                }
            }

            [Fact]
            public void ShouldSkipProfilesWhenLoadingImage()
            {
                var settings = new MagickReadSettings
                {
                    Defines = new PngReadDefines
                    {
                        SkipProfiles = PngProfileTypes.Xmp | PngProfileTypes.Exif,
                    },
                };

                using (var image = new MagickImage())
                {
                    image.Read(Files.FujiFilmFinePixS1ProPNG);
                    Assert.NotNull(image.GetExifProfile());
                    Assert.NotNull(image.GetXmpProfile());

                    image.Read(Files.FujiFilmFinePixS1ProPNG, settings);
                    Assert.Null(image.GetExifProfile());
                    Assert.Null(image.GetXmpProfile());
                }
            }
        }
    }
}
