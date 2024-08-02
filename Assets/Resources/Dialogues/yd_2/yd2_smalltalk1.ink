->DL
== DL
//描述：林象辰与排队的鬼火交流1
//触发：主动
//形式：文本框（有选项）
头顶一直在滴水，快把我淋没了。#Speaker:GH1 #Name:鬼火 #Layout:Right

+[是啊，一直没人来修，冷飕飕的。]#Speaker:LXC #Name:林象辰 #Layout: Left
->R1

+[您放心，已经报修到有关部门。]#Speaker:LXC #Name:林象辰 #Layout: Left
->R2

+[有屋顶就不错了，再抱怨给你赶出去。]#Speaker:LXC #Name:林象辰 #Layout: Left
->R3

==R1
（冷）抖抖抖。#Speaker:GH1 #Name:鬼火 #Layout:Right
->DONE

==R2
（呲拉——，火势更小了）#Speaker:GH1 #Name:鬼火 #Layout:Right
->DONE

==R3
（害怕）抖抖抖。#Speaker:GH1 #Name:鬼火 #Layout:Right
->DONE
