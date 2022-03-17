using System.Collections.Generic;
using UnityEngine;
using Update;

namespace Features.GameUpdate
{
    namespace Features.GameUpdate
    {
        public class GameUpdates : MonoBehaviour
        {
            private List<IUpdate> _updates;
            private bool _isStopped = true;

            private void Awake()
            {
                _updates = new List<IUpdate>();
            }

            public void AddToUpdateList(IUpdate gameUpdate)
            {
                _updates.Add(gameUpdate);
            }

            private void RemoveFromUpdateList(IUpdate gameUpdate)
            {
                var index = _updates.FindIndex(s => s == gameUpdate);
                var lastIndex = _updates.Count - 1;
                _updates[index] = _updates[lastIndex];
                _updates.RemoveAt(lastIndex);
            }

            private void Update()
            {
                if (_isStopped) 
                    return;

                for (var i = 0; i < _updates.Count; i++)
                {
                    _updates[i].Update();
                }
            }

            public void StopUpdate()
            {
                _isStopped = true;
            }

            public void ResumeUpdate()
            {
                _isStopped = false;
            }
        }
    }
}