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
    鬼魂：什么叫下次一定，等等……
    ->END
  
== guiying
    // 归阴对话写在下方
    鬼魂：记得来看我的阴间首秀！
    金翎（秦广王）：此人阳寿未尽，应当返阳！
    ->END

== yijiao
    // 移交对话写在下 第一天无移交按钮
    移交
    ->END

== nohandle
    // 不处理对话写在下
    不处理
    
->END
