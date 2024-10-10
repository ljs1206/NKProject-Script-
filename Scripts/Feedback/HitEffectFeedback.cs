using System.Collections;
using System.Collections.Generic;
using ObjectPooling;
using UnityEngine;

public class HitEffectFeedback : Feedback
{
    public override void CreateFeedback()
    {
        var effect = PoolManager.Instance.Pop(PoolingType.SwordHitEffect) as HitEffect;
        ActionData actionData = _owner.HealthCompo.ActionData;
        
        effect.transform.position = actionData._hitInfo.hitPoint;
        // 회전은 나중에 추가해야할듯 아마도
        
        // todo 나중에 이펙트 스크립트도 상속구조로 변경해야됨
        // todo (이펙트가 Pool처리 될 예정이라서 스크립틀 하나씩 필요함 외부에서 불러오는 일도 자주 있어서 따로 구조 쌓아야할듯??)
        effect.ResetItem();
    }

    public override void FinishFeedback()
    {
        
    }
}
