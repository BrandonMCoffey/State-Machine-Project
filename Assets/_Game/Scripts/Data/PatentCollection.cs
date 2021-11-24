using System.Collections.Generic;
using System.Linq;
using GridTool.DataScripts;
using UnityEngine;
using Utility.Buttons;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Scripts.Data
{
    [CreateAssetMenu(menuName = "TM/Patent Collection")]
    public class PatentCollection : ScriptableObject
    {
        [SerializeField] [ReadOnly] private List<PatentData> _patents = new List<PatentData>();

        [SerializeField] private bool _allowDuplicates = true;

        [SerializeField] [ReadOnly] private List<PatentData> _availablePatents = new List<PatentData>();

        [SerializeField] [ReadOnly] private List<PatentData> _discardedPatents = new List<PatentData>();

        public List<PatentData> AllPatents => _patents;
        public List<PatentData> AvailablePatents => _availablePatents;
        public List<PatentData> DiscardedPatents => _discardedPatents;

        public void RestoreList()
        {
            _availablePatents = _patents;
            if (!_allowDuplicates) {
                _availablePatents = _availablePatents.Distinct().ToList();
            }
            _discardedPatents = new List<PatentData>();
            VerifyPatents();
        }

        public void RestoreDiscardedList()
        {
            foreach (var patent in _discardedPatents) {
                _availablePatents.Add(patent);
            }
            _discardedPatents = new List<PatentData>();
        }

        public PatentData GetRandom()
        {
            if (_availablePatents.Count == 0) return null;
            int rand = Random.Range(0, _availablePatents.Count);
            var patent = _availablePatents[rand];
            _availablePatents.RemoveAt(rand);
            return patent;
        }

        public List<PatentData> GetRandom(int count)
        {
            var patents = new List<PatentData>(count);
            for (int i = 0; i < count; i++) {
                patents.Add(GetRandom());
            }
            return patents;
        }

        public void AddToDiscarded(PatentData patent)
        {
            _discardedPatents.Add(patent);
        }

        private void VerifyPatents()
        {
            _patents = _patents.Where(patent => patent != null).ToList();
            _availablePatents = _availablePatents.Where(patent => patent != null).ToList();
            _discardedPatents = _discardedPatents.Where(patent => patent != null).ToList();
        }


#if UNITY_EDITOR
        private void OnValidate()
        {
            VerifyPatents();
        }

        [ContextMenu("Make New")]
        private void EditorMakeNewPatent()
        {
            EditorButtonMakeNewPatent("New Patent");
        }

        [Button("Create New Patent", Spacing = 10)]
        private void EditorButtonMakeNewPatent(string patentName)
        {
            PatentData patent = CreateInstance<PatentData>();
            patent.name = patentName;
            patent.Name = patentName;
            patent.Init(this);
            _patents.Add(patent);

            AssetDatabase.AddObjectToAsset(patent, this);
            AssetDatabase.SaveAssets();

            EditorUtility.SetDirty(this);
            EditorUtility.SetDirty(patent);
        }

        [ContextMenu("Delete All")]
        private void EditorDeleteAllPatents()
        {
            for (int i = _patents.Count - 1; i >= 0; i--) {
                var tmp = _patents[i];
                _patents.Remove(tmp);
                Undo.DestroyObjectImmediate(tmp);
            }
            AssetDatabase.SaveAssets();
        }

        public void EditorDeletePatent(PatentData patent)
        {
            _patents.Remove(patent);
            Undo.DestroyObjectImmediate(patent);
            AssetDatabase.SaveAssets();
        }
#endif
    }
}