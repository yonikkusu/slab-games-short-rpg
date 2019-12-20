#!/bin/bash

magick convert $1.png -crop 24x24 croped%02d.png

# 8方向隣接している
magick montage croped18.png croped17.png croped14.png croped13.png -geometry 24x24 -background none -tile 2x2 tmp00.png

# ななめ1方向以外隣接している
magick montage croped02.png croped17.png croped14.png croped13.png -geometry 24x24 -background none -tile 2x2 tmp01.png
magick montage croped18.png croped03.png croped14.png croped13.png -geometry 24x24 -background none -tile 2x2 tmp02.png
magick montage croped18.png croped17.png croped06.png croped13.png -geometry 24x24 -background none -tile 2x2 tmp03.png
magick montage croped18.png croped17.png croped14.png croped07.png -geometry 24x24 -background none -tile 2x2 tmp04.png

# ななめ2方向以外隣接している
magick montage croped02.png croped03.png croped14.png croped13.png -geometry 24x24 -background none -tile 2x2 tmp05.png
magick montage croped18.png croped17.png croped06.png croped07.png -geometry 24x24 -background none -tile 2x2 tmp06.png
magick montage croped02.png croped17.png croped06.png croped13.png -geometry 24x24 -background none -tile 2x2 tmp07.png
magick montage croped18.png croped03.png croped14.png croped07.png -geometry 24x24 -background none -tile 2x2 tmp08.png
magick montage croped02.png croped17.png croped14.png croped07.png -geometry 24x24 -background none -tile 2x2 tmp09.png
magick montage croped18.png croped03.png croped06.png croped13.png -geometry 24x24 -background none -tile 2x2 tmp10.png

# ななめ3方向以外隣接している
magick montage croped02.png croped03.png croped06.png croped13.png -geometry 24x24 -background none -tile 2x2 tmp11.png
magick montage croped02.png croped03.png croped14.png croped07.png -geometry 24x24 -background none -tile 2x2 tmp12.png
magick montage croped02.png croped17.png croped06.png croped07.png -geometry 24x24 -background none -tile 2x2 tmp13.png
magick montage croped18.png croped03.png croped06.png croped07.png -geometry 24x24 -background none -tile 2x2 tmp14.png

# ななめ4方向以外隣接している
magick montage croped02.png croped03.png croped06.png croped07.png -geometry 24x24 -background none -tile 2x2 tmp15.png

#上3方向は隣接していない
magick montage croped10.png croped09.png croped14.png croped13.png -geometry 24x24 -background none -tile 2x2 tmp16.png
magick montage croped10.png croped09.png croped06.png croped13.png -geometry 24x24 -background none -tile 2x2 tmp17.png
magick montage croped10.png croped09.png croped14.png croped07.png -geometry 24x24 -background none -tile 2x2 tmp18.png
magick montage croped10.png croped09.png croped06.png croped07.png -geometry 24x24 -background none -tile 2x2 tmp19.png

#下3方向は隣接していない
magick montage croped18.png croped17.png croped22.png croped21.png -geometry 24x24 -background none -tile 2x2 tmp20.png
magick montage croped02.png croped17.png croped22.png croped21.png -geometry 24x24 -background none -tile 2x2 tmp21.png
magick montage croped18.png croped03.png croped22.png croped21.png -geometry 24x24 -background none -tile 2x2 tmp22.png
magick montage croped02.png croped03.png croped22.png croped21.png -geometry 24x24 -background none -tile 2x2 tmp23.png

#左3方向は隣接していない
magick montage croped16.png croped17.png croped12.png croped13.png -geometry 24x24 -background none -tile 2x2 tmp24.png
magick montage croped16.png croped03.png croped12.png croped13.png -geometry 24x24 -background none -tile 2x2 tmp25.png
magick montage croped16.png croped17.png croped12.png croped07.png -geometry 24x24 -background none -tile 2x2 tmp26.png
magick montage croped16.png croped03.png croped12.png croped07.png -geometry 24x24 -background none -tile 2x2 tmp27.png

#右3方向は隣接していない
magick montage croped18.png croped19.png croped14.png croped15.png -geometry 24x24 -background none -tile 2x2 tmp28.png
magick montage croped02.png croped19.png croped14.png croped15.png -geometry 24x24 -background none -tile 2x2 tmp29.png
magick montage croped18.png croped19.png croped06.png croped15.png -geometry 24x24 -background none -tile 2x2 tmp30.png
magick montage croped02.png croped19.png croped06.png croped15.png -geometry 24x24 -background none -tile 2x2 tmp31.png

#角2方向
magick montage croped08.png croped09.png croped12.png croped13.png -geometry 24x24 -background none -tile 2x2 tmp32.png
magick montage croped08.png croped09.png croped12.png croped07.png -geometry 24x24 -background none -tile 2x2 tmp33.png
magick montage croped10.png croped11.png croped14.png croped15.png -geometry 24x24 -background none -tile 2x2 tmp34.png
magick montage croped10.png croped11.png croped06.png croped15.png -geometry 24x24 -background none -tile 2x2 tmp35.png
magick montage croped16.png croped17.png croped20.png croped21.png -geometry 24x24 -background none -tile 2x2 tmp36.png
magick montage croped16.png croped03.png croped20.png croped21.png -geometry 24x24 -background none -tile 2x2 tmp37.png
magick montage croped18.png croped15.png croped22.png croped23.png -geometry 24x24 -background none -tile 2x2 tmp38.png
magick montage croped02.png croped15.png croped22.png croped23.png -geometry 24x24 -background none -tile 2x2 tmp39.png

# 縦
magick montage croped16.png croped19.png croped12.png croped15.png -geometry 24x24 -background none -tile 2x2 tmp40.png

# 横
magick montage croped10.png croped09.png croped22.png croped21.png -geometry 24x24 -background none -tile 2x2 tmp41.png

# 1方向のみ隣接していない
magick montage croped16.png croped19.png croped04.png croped05.png -geometry 24x24 -background none -tile 2x2 tmp42.png
magick montage croped00.png croped01.png croped12.png croped15.png -geometry 24x24 -background none -tile 2x2 tmp43.png
magick montage croped10.png croped01.png croped22.png croped05.png -geometry 24x24 -background none -tile 2x2 tmp44.png
magick montage croped00.png croped09.png croped04.png croped21.png -geometry 24x24 -background none -tile 2x2 tmp45.png

# 全方向隣接していない
magick montage croped00.png croped01.png croped04.png croped05.png -geometry 24x24 -background none -tile 2x2 tmp46.png

#生成した画像を結合
magick montage tmp*.png -geometry 48x48 -background none -tile 8x6 out_$1.png

del croped*.png tmp*.png
