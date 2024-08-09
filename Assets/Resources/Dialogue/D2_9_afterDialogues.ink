VAR choice = 0

    {
        -choice == 1:  
            -> fanyang  
        -choice == 2:  
            -> guiying 
        -choice == 3:
            -> yijiao
        -choice == 4:
            -> nohandle
    }  
  
== fanyang
    // 返阳对话写在下方
    鬼魂：不要啊！万圣节庆典更可怕。
    ->END
  
== guiying
    // 归阴对话写在下方
    鬼魂：看来无需过于惧怕死亡。
    金翎（秦广王）：死期未至，遣返人间。
    ->END

== yijiao
    // 移交对话写在下
    鬼魂：看来无需过于惧怕死亡。
    金翎（秦广王）：死期未至，遣返人间。
    ->END

== nohandle
    // 不处理对话写在下
    不处理
    
->END