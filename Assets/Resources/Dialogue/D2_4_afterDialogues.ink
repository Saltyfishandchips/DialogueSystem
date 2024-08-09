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
    鬼魂：I can go caving from the bottom up now.
    金翎（秦广王）：国籍有误，理应移交！
    ->END
  
== guiying
    // 归阴对话写在下方
    鬼魂：I can go caving from the bottom up now.
    金翎（秦广王）：国籍有误，理应移交！
    ->END

== yijiao
    // 移交对话写在下
    鬼魂：I'll be back.
    ->END

== nohandle
    // 不处理对话写在下
    不处理
    
->END