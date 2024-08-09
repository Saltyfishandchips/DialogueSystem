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
    // 反阳对话写在下方
    鬼魂：你好狠的心！
    ->END
  
== guiying
    // 归阴对话写在下方
    鬼魂：大感谢！之后一定要来我的趴哦。
    金翎（秦广王）：死期未至，即刻返阳。
    ->END

== yijiao
    // 移交对话写在下
    鬼魂：你好狠的心！
    金翎（秦广王）：死期未至，即刻返阳。
    ->END

== nohandle
    // 不处理对话写在下
    不处理
    
->END