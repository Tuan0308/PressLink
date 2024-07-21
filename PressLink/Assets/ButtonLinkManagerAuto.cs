using UnityEngine;
using UnityEngine.UI;

public class ButtonLinkManagerAuto : MonoBehaviour
{
    [System.Serializable]
    public class ButtonURL
    {
        public string name;
        public string url;
    }

    public GameObject parentObject; // GameObject chứa các nút con
    public ButtonURL[] buttonURLs; // Mảng URL cho các nút

    void Start()
    {
        UpdateButton();
    }

    public void UpdateButton()
    {
        if (parentObject != null)
        {
            Button[] buttons = parentObject.GetComponentsInChildren<Button>();
            // Xóa tất cả các listener cũ để tránh xung đột
            foreach (Button button in buttons)
            {
                button.onClick.RemoveAllListeners();
            }

            // Cập nhật liên kết cho các nút
            for (int i = 0; i < buttons.Length; i++)
            {
                int index = i; // Cần thiết để tránh lỗi closure trong lambda
                if (index < buttonURLs.Length)
                {
                    string url = buttonURLs[index].url; // Lưu trữ URL cục bộ để tránh lỗi closure
                    buttons[index].onClick.AddListener(() => OpenURL(url));
                }
                else
                {
                    Debug.LogWarning($"Chưa có URL được gán cho nút tại chỉ mục {index}");
                }
            }
        }
        else
        {
            Debug.LogError("Chưa gán đối tượng parent.");
        }
    }

    void OpenURL(string url)
    {
        Application.OpenURL(url);
    }
}
