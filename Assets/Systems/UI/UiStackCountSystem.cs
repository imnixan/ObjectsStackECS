using Leopotam.Ecs;
using TMPro;
using UnityEngine;

public class UiStackCountSystem : IEcsInitSystem, IEcsRunSystem
{
    private UIData _uiData;
    private TextMeshProUGUI _stackCounter;
    private EcsFilter<PlayerTag, StackHolder> _filter = null;

    public void Init()
    {
        _stackCounter = _uiData.CurrentStackCounter;
    }

    public void Run()
    {
        if (_filter.IsEmpty())
        {
            return;
        }

        foreach (int index in _filter)
        {
            ref EcsEntity entity = ref _filter.GetEntity(index);
            var stackHolder = entity.Get<StackHolder>();
            _stackCounter.text = stackHolder.CurrentStack.ToString();
        }
    }
}
