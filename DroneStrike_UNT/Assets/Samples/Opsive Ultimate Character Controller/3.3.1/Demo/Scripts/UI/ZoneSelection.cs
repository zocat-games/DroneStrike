/// ---------------------------------------------
/// Ultimate Character Controller
/// Copyright (c) Opsive. All Rights Reserved.
/// https://www.opsive.com
/// ---------------------------------------------

namespace Opsive.UltimateCharacterController.Demo.UI
{
    using Opsive.Shared.Events;
    using Opsive.Shared.Game;
    using UnityEngine;
    using UnityEngine.EventSystems;
#if ENABLE_INPUT_SYSTEM
    using UnityEngine.InputSystem;
#endif
    using UnityEngine.Rendering;
    using UnityEngine.UI;

    /// <summary>
    /// The ZoneSelection component will manage the ZoneElements within the ScrollRect.
    /// </summary>
    public class ZoneSelection : MonoBehaviour
    {
        [Tooltip("A reference to the UI container for the zone.")]
        [SerializeField] protected GameObject m_Container;
#if ULTIMATE_CHARACTER_CONTROLLER_UNIVERSALRP
        [Tooltip("A reference to the post processing volume")]
        [SerializeField] protected Volume m_PostProcessingVolume;
        [Tooltip("A reference to the profile that should be used when the menu is shown.")]
        [SerializeField] protected VolumeProfile m_MenuPostProcessingProfile;
#endif
        [Tooltip("A reference to the scrollrect used to select zones.")]
        [SerializeField] protected ScrollRect m_ZoneScrollRect;
        [Tooltip("A reference to the prefab used for each zone selection element.")]
        [SerializeField] protected GameObject m_ZoneElementPrefab;
        [Tooltip("A reference to the selected zone header text.")]
        [SerializeField] protected Shared.UI.Text m_SelectedZoneHeader;
        [Tooltip("A reference to the selected zone image.")]
        [SerializeField] protected Image m_SelectedZoneImage;
        [Tooltip("A reference to the selected zone description.")]
        [SerializeField] protected Shared.UI.Text m_SelectedZoneDescription;
        [Tooltip("A reference to the auto load zone parent text.")]
        [SerializeField] protected Shared.UI.Text m_NextZoneSelectionText;
        [Tooltip("A reference to the resume button.")]
        [SerializeField] protected GameObject m_ResumeButton;
        [Tooltip("The amount of time before the next zone selection is automatically made. Set to -1 to disable.")]
        [SerializeField] protected float m_NextZoneSelectionTimeout = 5;

        public GameObject Container { get => m_Container; }
        public ScrollRect ZoneScrollRect { get => m_ZoneScrollRect; }

        private DemoManager m_DemoManager;

        private float m_ScrollRectHeight;
        private float m_ZoneElementHeight;
        private int m_SelectedZoneIndex;
        private ScheduledEventBase m_AutoSelectionEvent;
        private string m_StartZoneText;
        private int m_CanActivateFrame = -1;

        public int SelectedZoneIndex { get => m_SelectedZoneIndex; }

        /// <summary>
        /// Initialize the default values.
        /// </summary>
        private void Awake()
        {
#if UNITY_2023_1_OR_NEWER
            m_DemoManager = Object.FindFirstObjectByType<DemoManager>();
#else
            m_DemoManager = Object.FindObjectOfType<DemoManager>();
#endif

            m_StartZoneText = m_NextZoneSelectionText.text;
            m_NextZoneSelectionText.gameObject.SetActive(false);

            var demoZones = m_DemoManager.DemoZones;
            for (int i = 0; i < demoZones.Length; ++i) {
                if (m_DemoManager.DemoZones[i] == null) {
                    continue;
                }

                m_DemoManager.DemoZones[i].ZoneElement = Object.Instantiate(m_ZoneElementPrefab).GetComponent<ZoneElement>();
                m_DemoManager.DemoZones[i].ZoneElement.Initialize(m_DemoManager.DemoZones[i], this, i);
            }

            m_ZoneElementHeight = m_ZoneElementPrefab.GetCachedComponent<RectTransform>().rect.height;
            m_ZoneScrollRect.content.sizeDelta = new Vector2(m_ZoneScrollRect.content.sizeDelta.x, demoZones.Length * m_ZoneElementPrefab.GetComponent<RectTransform>().sizeDelta.y);
            m_ScrollRectHeight = m_ZoneScrollRect.GetComponent<RectTransform>().rect.height;
            m_ResumeButton?.SetActive(false);
#if ULTIMATE_CHARACTER_CONTROLLER_UNIVERSALRP
            if (!m_DemoManager.FreeRoam) {
                m_PostProcessingVolume.profile = m_MenuPostProcessingProfile;
            }
#endif
        }

        /// <summary>
        /// Shows or hides the menu.
        /// </summary>
        /// <param name="show">Should the menu be shown?</param>
        public void ShowMenu(bool show)
        {
            ShowMenu(show, true);
        }

        /// <summary>
        /// Shows or hides the menu.
        /// </summary>
        /// <param name="show">Should the menu be shown?</param>
        /// <param name="nextZone">Should the next zone be selected?</param>
        public void ShowMenu(bool show, bool nextZone)
        {
            if (show && m_DemoManager == null) {
                Debug.LogWarning("Warning: The ZoneSelection component has not been initialized and cannot show. Ensure the parent canvas GameObject is active before playing.");
                return;
            }

            if (show && Time.frameCount < m_CanActivateFrame) {
                return;
            } else if (!show) {
                if (!nextZone && m_DemoManager.LastZoneIndex == -1) {
                    return;
                }
                m_CanActivateFrame = Time.frameCount + 1;
            }

            m_Container.SetActive(show);
            m_ResumeButton.SetActive(show && !nextZone);
            m_NextZoneSelectionText.gameObject.SetActive(show && nextZone && m_NextZoneSelectionTimeout >= 0);
            m_DemoManager.enabled = !show;
            EventHandler.ExecuteEvent(m_DemoManager.Character, "OnEnableGameplayInput", !show);
            Cursor.visible = show;
            Cursor.lockState = Cursor.visible ? CursorLockMode.None : CursorLockMode.Locked;
#if ULTIMATE_CHARACTER_CONTROLLER_UNIVERSALRP
            m_PostProcessingVolume.profile = show ? m_MenuPostProcessingProfile : null;
#endif

            if (show && m_NextZoneSelectionText.gameObject.activeSelf) {
                EventSystem.current.sendNavigationEvents = false;
                if (nextZone) {
                    var nextZoneIndex = Mathf.Min(m_DemoManager.LastZoneIndex + 1, m_DemoManager.DemoZones.Length - 1);
                    FocusZone(nextZoneIndex);
                    m_AutoSelectionEvent = Scheduler.Schedule<int>(m_NextZoneSelectionTimeout, LoadZone, nextZoneIndex);
                } else {
                    FocusZone(m_DemoManager.LastZoneIndex);
                }
            } else {
                CancelAutoSelection();
                if (show) {
                    FocusZone(m_DemoManager.LastZoneIndex);
                }
            }
            m_DemoManager.ZoneSelectionChange(show);
        }

        /// <summary>
        /// Cancels the auto zone selection.
        /// </summary>
        private void CancelAutoSelection()
        {
            if (m_AutoSelectionEvent != null) {
                Scheduler.Cancel(m_AutoSelectionEvent);
                m_AutoSelectionEvent = null;
                m_NextZoneSelectionText.gameObject.SetActive(false);
            }
        }

        /// <summary>
        /// Perform any per frame updates related to navigation and UI.
        /// </summary>
        public void Update()
        {
            if ((m_DemoManager.LastZoneIndex != -1 || m_DemoManager.FreeRoam) &&
#if ENABLE_INPUT_SYSTEM
                (Keyboard.current?.escapeKey.wasPressedThisFrame ?? false)
#else
                Input.GetKeyDown(KeyCode.Escape)
#endif
                ) {
                ShowMenu(false, false);
                return;
            }

            // After the menu is loaded again the vertical buttons may still be pressed and change the selection.
            // Enable the navigation after the input is clear.
            if (!EventSystem.current.sendNavigationEvents &&
#if ENABLE_INPUT_SYSTEM
                    !(Keyboard.current?.wKey.isPressed ?? false) && !(Keyboard.current?.sKey.isPressed ?? false) &&
                    !(Keyboard.current?.upArrowKey.isPressed ?? false) && !(Keyboard.current?.downArrowKey.isPressed ?? false)

#else
                    !Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S) && 
                    !Input.GetKeyDown(KeyCode.UpArrow) && !Input.GetKeyDown(KeyCode.DownArrow)
#endif
                    ) {
                EventSystem.current.sendNavigationEvents = true;
            }

            // Load the zone with the return and spacebar inputs.
#if ENABLE_INPUT_SYSTEM
            if ((Keyboard.current?.enterKey.wasPressedThisFrame ?? false) || (Keyboard.current?.numpadEnterKey.wasPressedThisFrame ?? false) || (Keyboard.current?.spaceKey.wasPressedThisFrame ?? false)) {
#else
            if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space)) {
#endif
                LoadZone(m_SelectedZoneIndex);
            }

            if (m_AutoSelectionEvent == null) {
                return;
            }

            m_NextZoneSelectionText.text = string.Format(m_StartZoneText, Mathf.RoundToInt((float)(m_AutoSelectionEvent.EndTime - Time.time)));
        }

        /// <summary>
        /// Enables focus on the selected zone.
        /// </summary>
        /// <param name="index">The index of the zone.</param>
        public void FocusZone(int index)
        {
            CancelAutoSelection();

            m_DemoManager.DemoZones[index].ZoneElement.Select();
            m_SelectedZoneIndex = index;

            m_SelectedZoneHeader.text = m_DemoManager.DemoZones[index].Header;
            m_SelectedZoneImage.sprite = m_DemoManager.DemoZones[index].Sprite;
            m_SelectedZoneDescription.text = m_DemoManager.DemoZones[index].Description;

            // Scroll to the element if the element is not within view.
            var targetPosition = index * m_ZoneElementHeight;
            var currentPosition = m_ZoneScrollRect.content.anchoredPosition.y;
            if (targetPosition < currentPosition || targetPosition > currentPosition + m_ScrollRectHeight - m_ZoneElementHeight) {
                var anchoredPosition = m_ZoneScrollRect.content.anchoredPosition;
                anchoredPosition.y = targetPosition - (targetPosition > currentPosition + m_ScrollRectHeight - m_ZoneElementHeight ? m_ScrollRectHeight - m_ZoneElementHeight : 0);
                m_ZoneScrollRect.content.anchoredPosition = anchoredPosition;
            }
        }

        /// <summary>
        /// Loads the zone.
        /// </summary>
        /// <param name="index">The idnex of the zone.</param>
        public void LoadZone(int index)
        {
            EventSystem.current.SetSelectedGameObject(null);
            if (m_DemoManager.Teleport(index)) {
                m_SelectedZoneIndex = m_DemoManager.LastZoneIndex = index;
            }
            ShowMenu(false);
        }

        /// <summary>
        /// Reverts the ZoneSelection to the current zone index.
        /// </summary>
        public void RevertSelection()
        {
            FocusZone(m_DemoManager.LastZoneIndex >= 0 ? m_DemoManager.LastZoneIndex : 0);
        }

        /// <summary>
        /// The component has been disabled.
        /// </summary>
        private void OnDisable()
        {
            CancelAutoSelection();
        }
    }
}