#!/bin/bash

# 画像切り抜き
magick convert $1/$1.png -crop 24x24 $1/croped%02d.png

# 全方向隣接していない
magick montage $1/croped00.png $1/croped03.png $1/croped12.png $1/croped15.png -geometry 24x24 -background none -tile 2x2 $1/tmp00.png

# 8方向隣接している
magick montage $1/croped06.png $1/croped05.png $1/croped09.png $1/croped10.png -geometry 24x24 -background none -tile 2x2 $1/tmp01.png

# 角
magick montage $1/croped00.png $1/croped01.png $1/croped04.png $1/croped05.png -geometry 24x24 -background none -tile 2x2 $1/tmp02.png
magick montage $1/croped02.png $1/croped03.png $1/croped06.png $1/croped07.png -geometry 24x24 -background none -tile 2x2 $1/tmp03.png
magick montage $1/croped08.png $1/croped09.png $1/croped12.png $1/croped13.png -geometry 24x24 -background none -tile 2x2 $1/tmp04.png
magick montage $1/croped10.png $1/croped11.png $1/croped14.png $1/croped15.png -geometry 24x24 -background none -tile 2x2 $1/tmp05.png

#上方向のみ隣接していない
magick montage $1/croped01.png $1/croped02.png $1/croped05.png $1/croped06.png -geometry 24x24 -background none -tile 2x2 $1/tmp06.png

#下方向のみ隣接していない
magick montage $1/croped09.png $1/croped10.png $1/croped13.png $1/croped14.png -geometry 24x24 -background none -tile 2x2 $1/tmp07.png

#左方向のみ隣接していない
magick montage $1/croped04.png $1/croped05.png $1/croped08.png $1/croped09.png -geometry 24x24 -background none -tile 2x2 $1/tmp08.png

#右方向のみ隣接していない
magick montage $1/croped06.png $1/croped07.png $1/croped10.png $1/croped11.png -geometry 24x24 -background none -tile 2x2 $1/tmp09.png

# 縦
magick montage $1/croped04.png $1/croped07.png $1/croped08.png $1/croped11.png -geometry 24x24 -background none -tile 2x2 $1/tmp10.png

# 横
magick montage $1/croped01.png $1/croped02.png $1/croped13.png $1/croped14.png -geometry 24x24 -background none -tile 2x2 $1/tmp11.png

# 1方向しか隣接していない
magick montage $1/croped08.png $1/croped11.png $1/croped12.png $1/croped15.png -geometry 24x24 -background none -tile 2x2 $1/tmp12.png
magick montage $1/croped00.png $1/croped01.png $1/croped12.png $1/croped13.png -geometry 24x24 -background none -tile 2x2 $1/tmp13.png
magick montage $1/croped00.png $1/croped03.png $1/croped04.png $1/croped07.png -geometry 24x24 -background none -tile 2x2 $1/tmp14.png
magick montage $1/croped02.png $1/croped03.png $1/croped14.png $1/croped15.png -geometry 24x24 -background none -tile 2x2 $1/tmp15.png

#生成した画像を結合
magick montage $1/tmp*.png -geometry 48x48 -background none -tile 8x2 $1/out_$1.png

rm $1/croped*.png $1/croped*.png.meta $1/tmp*.png $1/tmp*.png.meta
