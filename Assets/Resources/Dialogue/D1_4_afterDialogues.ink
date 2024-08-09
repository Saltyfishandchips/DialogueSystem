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
    鬼魂：呵呵…呵。
    金翎（秦广王）：弄错了，抓回来！
    ->END
  
== guiying
    // 归阴对话写在下方
    鬼魂：年轻人，记得照顾好自己。
    ->END

== yijiao
    // 移交对话写在下 第一天无移交按钮
    移交
    ->END

== nohandle
    // 不处理对话写在下
    不处理
    
->END


