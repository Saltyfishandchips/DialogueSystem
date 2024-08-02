using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObituaryAnimation : MonoBehaviour
{
    public Animator myAnimator;  // Animator 组件
    public SpriteRenderer spriteRenderer;

     void Start()
    {
        if ( myAnimator == null)
        {
            Debug.LogError("Animator is not assigned.");
            return;
        }
    }

    public void OnPageDownBtnClicked()
    {
        //spriteRenderer.sortingOrder = 5;
        // 设置触发器或直接播放动画
        //myAnimator.SetTrigger("pDown");

        AudioManager.Instance.PlaySFX("PageTurning");
        // 翻页逻辑
        if(FlowDataManager.Instance.obituaryTotal.currentPage < FlowDataManager.Instance.obituaryTotal.totalPageNum)
        {
            FlowDataManager.Instance.obituaryTotal.currentPage++;
            FlowDataManager.Instance.pageTurning();
        }
    }

    public void OnPageUpBtnClicked()
    {
        //spriteRenderer.sortingOrder = 5;
        // 设置触发器或直接播放动画
        //myAnimator.SetTrigger("pUp");

        if(FlowDataManager.Instance.obituaryTotal.currentPage > 1)
        {
            FlowDataManager.Instance.obituaryTotal.currentPage--;
            FlowDataManager.Instance.pageTurning();
        }

        AudioManager.Instance.PlaySFX("PageTurning");

    }

    private System.Collections.IEnumerator WaitAndExecute(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
    }

    
    private void OnAnimationEnd(int i)
    {
        //spriteRenderer.sortingOrder = i;
    }

}
