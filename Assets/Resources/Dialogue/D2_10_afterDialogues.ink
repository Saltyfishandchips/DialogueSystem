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
    鬼魂：你可真好！等我学会假死就来救你出去！
    ->END
  
== guiying
    // 归阴对话写在下方
    鬼魂：那我进去以后，要向本人请教！
    金翎（秦广王）：诞辰有误，理应返阳！
    ->END

== yijiao
    // 移交对话写在下
    鬼魂：那我进去以后，要向本人请教！
    金翎（秦广王）：诞辰有误，理应返阳！
    ->END

== nohandle
    // 不处理对话写在下
    不处理
    
->END