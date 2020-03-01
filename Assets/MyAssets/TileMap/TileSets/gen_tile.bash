#!/bin/bash

# 画像切り抜き
magick convert $1/$1.png -crop 24x24 $1/croped%02d.png

# 8方向隣接している
magick montage $1/croped18.png $1/croped17.png $1/croped14.png $1/croped13.png -geometry 24x24 -background none -tile 2x2 $1/tmp00.png

# ななめ1方向以外隣接している
magick montage $1/croped02.png $1/croped17.png $1/croped14.png $1/croped13.png -geometry 24x24 -background none -tile 2x2 $1/tmp01.png
magick montage $1/croped18.png $1/croped03.png $1/croped14.png $1/croped13.png -geometry 24x24 -background none -tile 2x2 $1/tmp02.png
magick montage $1/croped18.png $1/croped17.png $1/croped06.png $1/croped13.png -geometry 24x24 -background none -tile 2x2 $1/tmp03.png
magick montage $1/croped18.png $1/croped17.png $1/croped14.png $1/croped07.png -geometry 24x24 -background none -tile 2x2 $1/tmp04.png

# ななめ2方向以外隣接している
magick montage $1/croped02.png $1/croped03.png $1/croped14.png $1/croped13.png -geometry 24x24 -background none -tile 2x2 $1/tmp05.png
magick montage $1/croped18.png $1/croped17.png $1/croped06.png $1/croped07.png -geometry 24x24 -background none -tile 2x2 $1/tmp06.png
magick montage $1/croped02.png $1/croped17.png $1/croped06.png $1/croped13.png -geometry 24x24 -background none -tile 2x2 $1/tmp07.png
magick montage $1/croped18.png $1/croped03.png $1/croped14.png $1/croped07.png -geometry 24x24 -background none -tile 2x2 $1/tmp08.png
magick montage $1/croped02.png $1/croped17.png $1/croped14.png $1/croped07.png -geometry 24x24 -background none -tile 2x2 $1/tmp09.png
magick montage $1/croped18.png $1/croped03.png $1/croped06.png $1/croped13.png -geometry 24x24 -background none -tile 2x2 $1/tmp10.png

# ななめ3方向以外隣接している
magick montage $1/croped02.png $1/croped03.png $1/croped06.png $1/croped13.png -geometry 24x24 -background none -tile 2x2 $1/tmp11.png
magick montage $1/croped02.png $1/croped03.png $1/croped14.png $1/croped07.png -geometry 24x24 -background none -tile 2x2 $1/tmp12.png
magick montage $1/croped02.png $1/croped17.png $1/croped06.png $1/croped07.png -geometry 24x24 -background none -tile 2x2 $1/tmp13.png
magick montage $1/croped18.png $1/croped03.png $1/croped06.png $1/croped07.png -geometry 24x24 -background none -tile 2x2 $1/tmp14.png

# ななめ4方向以外隣接している
magick montage $1/croped02.png $1/croped03.png $1/croped06.png $1/croped07.png -geometry 24x24 -background none -tile 2x2 $1/tmp15.png

#上3方向は隣接していない
magick montage $1/croped10.png $1/croped09.png $1/croped14.png $1/croped13.png -geometry 24x24 -background none -tile 2x2 $1/tmp16.png
magick montage $1/croped10.png $1/croped09.png $1/croped06.png $1/croped13.png -geometry 24x24 -background none -tile 2x2 $1/tmp17.png
magick montage $1/croped10.png $1/croped09.png $1/croped14.png $1/croped07.png -geometry 24x24 -background none -tile 2x2 $1/tmp18.png
magick montage $1/croped10.png $1/croped09.png $1/croped06.png $1/croped07.png -geometry 24x24 -background none -tile 2x2 $1/tmp19.png

#下3方向は隣接していない
magick montage $1/croped18.png $1/croped17.png $1/croped22.png $1/croped21.png -geometry 24x24 -background none -tile 2x2 $1/tmp20.png
magick montage $1/croped02.png $1/croped17.png $1/croped22.png $1/croped21.png -geometry 24x24 -background none -tile 2x2 $1/tmp21.png
magick montage $1/croped18.png $1/croped03.png $1/croped22.png $1/croped21.png -geometry 24x24 -background none -tile 2x2 $1/tmp22.png
magick montage $1/croped02.png $1/croped03.png $1/croped22.png $1/croped21.png -geometry 24x24 -background none -tile 2x2 $1/tmp23.png

#左3方向は隣接していない
magick montage $1/croped16.png $1/croped17.png $1/croped12.png $1/croped13.png -geometry 24x24 -background none -tile 2x2 $1/tmp24.png
magick montage $1/croped16.png $1/croped03.png $1/croped12.png $1/croped13.png -geometry 24x24 -background none -tile 2x2 $1/tmp25.png
magick montage $1/croped16.png $1/croped17.png $1/croped12.png $1/croped07.png -geometry 24x24 -background none -tile 2x2 $1/tmp26.png
magick montage $1/croped16.png $1/croped03.png $1/croped12.png $1/croped07.png -geometry 24x24 -background none -tile 2x2 $1/tmp27.png

#右3方向は隣接していない
magick montage $1/croped18.png $1/croped19.png $1/croped14.png $1/croped15.png -geometry 24x24 -background none -tile 2x2 $1/tmp28.png
magick montage $1/croped02.png $1/croped19.png $1/croped14.png $1/croped15.png -geometry 24x24 -background none -tile 2x2 $1/tmp29.png
magick montage $1/croped18.png $1/croped19.png $1/croped06.png $1/croped15.png -geometry 24x24 -background none -tile 2x2 $1/tmp30.png
magick montage $1/croped02.png $1/croped19.png $1/croped06.png $1/croped15.png -geometry 24x24 -background none -tile 2x2 $1/tmp31.png

#角2方向
magick montage $1/croped08.png $1/croped09.png $1/croped12.png $1/croped13.png -geometry 24x24 -background none -tile 2x2 $1/tmp32.png
magick montage $1/croped08.png $1/croped09.png $1/croped12.png $1/croped07.png -geometry 24x24 -background none -tile 2x2 $1/tmp33.png
magick montage $1/croped10.png $1/croped11.png $1/croped14.png $1/croped15.png -geometry 24x24 -background none -tile 2x2 $1/tmp34.png
magick montage $1/croped10.png $1/croped11.png $1/croped06.png $1/croped15.png -geometry 24x24 -background none -tile 2x2 $1/tmp35.png
magick montage $1/croped16.png $1/croped17.png $1/croped20.png $1/croped21.png -geometry 24x24 -background none -tile 2x2 $1/tmp36.png
magick montage $1/croped16.png $1/croped03.png $1/croped20.png $1/croped21.png -geometry 24x24 -background none -tile 2x2 $1/tmp37.png
magick montage $1/croped18.png $1/croped15.png $1/croped22.png $1/croped23.png -geometry 24x24 -background none -tile 2x2 $1/tmp38.png
magick montage $1/croped02.png $1/croped15.png $1/croped22.png $1/croped23.png -geometry 24x24 -background none -tile 2x2 $1/tmp39.png

# 縦
magick montage $1/croped16.png $1/croped19.png $1/croped12.png $1/croped15.png -geometry 24x24 -background none -tile 2x2 $1/tmp40.png

# 横
magick montage $1/croped10.png $1/croped09.png $1/croped22.png $1/croped21.png -geometry 24x24 -background none -tile 2x2 $1/tmp41.png

# 1方向のみ隣接していない
magick montage $1/croped16.png $1/croped19.png $1/croped04.png $1/croped05.png -geometry 24x24 -background none -tile 2x2 $1/tmp42.png
magick montage $1/croped00.png $1/croped01.png $1/croped12.png $1/croped15.png -geometry 24x24 -background none -tile 2x2 $1/tmp43.png
magick montage $1/croped10.png $1/croped01.png $1/croped22.png $1/croped05.png -geometry 24x24 -background none -tile 2x2 $1/tmp44.png
magick montage $1/croped00.png $1/croped09.png $1/croped04.png $1/croped21.png -geometry 24x24 -background none -tile 2x2 $1/tmp45.png

# 全方向隣接していない
magick montage $1/croped00.png $1/croped01.png $1/croped04.png $1/croped05.png -geometry 24x24 -background none -tile 2x2 $1/tmp46.png

#生成した画像を結合
magick montage $1/tmp*.png -geometry 48x48 -background none -tile 8x6 $1/out_$1.png

rm $1/croped*.png $1/croped*.png.meta $1/tmp*.png $1/tmp*.png.meta
