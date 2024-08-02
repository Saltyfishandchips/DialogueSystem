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
   鬼魂：......
   林象辰：......回见。
    ->END
  
== guiying
    // 归阴对话写在下方
    鬼魂：......
    林象辰：......他怎么看起来怪怪的。
    金翎（秦广王）：名字不对，抓错人了！
    ->END

== yijiao
    // 移交对话写在下 第一天无移交按钮
    移交
    ->END

== nohandle
    // 不处理对话写在下
    不处理
    
->END

