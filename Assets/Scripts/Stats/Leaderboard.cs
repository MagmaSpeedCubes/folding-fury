using UnityEngine;
using TMPro;

// NOTE: Make sure to include the following namespace wherever you want to access Leaderboard Creator methods
using Dan.Main;

namespace LeaderboardCreatorDemo
{
    public class LeaderboardManager : MonoBehaviour
    {
        [SerializeField] private TMP_Text[] _entryTextObjects;
        [SerializeField] private TMP_InputField _usernameInputField;

// Make changes to this section according to how you're storing the player's score:

        
        private int Score => (int)GameInfo.Score;
// ------------------------------------------------------------

        private void Start()
        {
            LoadEntries();
        }

        private void LoadEntries()
        {
            // Q: How do I reference my own leaderboard?
            // A: Leaderboards.<NameOfTheLeaderboard>
        
            Leaderboards.test.GetEntries(entries =>
            {
                foreach (var t in _entryTextObjects)
                    t.text = "";

                var length = Mathf.Min(_entryTextObjects.Length, entries.Length);
                for (int i = 0; i < length; i++)
                    _entryTextObjects[i].text = $"{entries[i].Rank}. {entries[i].Username} - {entries[i].Score}";
            });
        }
        
        public void UploadEntry()
        {
            Leaderboards.test.UploadNewEntry(_usernameInputField.text, Score, isSuccessful =>
            {
                if (isSuccessful)
                    LoadEntries();
            });
        }
    }
}