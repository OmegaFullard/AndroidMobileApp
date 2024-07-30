
//Ads and Monetization (adverty), Rewards
*In app purchasing
*Ads
*pay for ad free
*unlock premium features 

//ShopCurrencyControllerEditor

using UnityEditor;
using UnityEngine;

namespace Figures
{
	[CustomEditor(typeof(ShopCurrencyController))]
	public class ShopCurrencyControllerEditor : Editor
	{
		private ShopCurrencyController _ctrl;

		private void OnEnable()
		{
			_ctrl = target as ShopCurrencyController;
		}

		public override void OnInspectorGUI()
		{
			DrawDefaultInspector();
			DrawCustomInspector();
		}

		private void DrawCustomInspector()
		{
			EditorGUILayout.Space();
			EditorGUILayout.Space();

			EditorGUILayout.BeginVertical("Box");
			{
				EditorGUILayout.BeginHorizontal();
				{
					_ctrl.FreeCurrencyDiapason.MinValue = EditorGUILayout.IntField((int)_ctrl.FreeCurrencyDiapason.MinValue, GUILayout.Width(30));

					GUILayout.FlexibleSpace();
					GUILayout.Label("Free Currency Diapason", EditorStyles.boldLabel);
					GUILayout.FlexibleSpace();

					_ctrl.FreeCurrencyDiapason.MaxValue = EditorGUILayout.IntField((int)_ctrl.FreeCurrencyDiapason.MaxValue, GUILayout.Width(30));
				}
				EditorGUILayout.EndHorizontal();

				EditorGUILayout.MinMaxSlider(ref _ctrl.FreeCurrencyDiapason.MinValue, ref _ctrl.FreeCurrencyDiapason.MaxValue, 1, 10);

				EditorGUILayout.BeginHorizontal();
				{
					_ctrl.AdsCurrencyDiapason.MinValue = EditorGUILayout.IntField((int)_ctrl.AdsCurrencyDiapason.MinValue, GUILayout.Width(30));

					GUILayout.FlexibleSpace();
					GUILayout.Label("Ads Currency Diapason", EditorStyles.boldLabel);
					GUILayout.FlexibleSpace();

					_ctrl.AdsCurrencyDiapason.MaxValue = EditorGUILayout.IntField((int)_ctrl.AdsCurrencyDiapason.MaxValue, GUILayout.Width(30));
				}
				EditorGUILayout.EndHorizontal();

				EditorGUILayout.MinMaxSlider(ref _ctrl.AdsCurrencyDiapason.MinValue, ref _ctrl.AdsCurrencyDiapason.MaxValue, 1, 10);
			}
			EditorGUILayout.EndVertical();
			SetEditorDirty();
		}

		private void SetEditorDirty()
		{
			if (GUI.changed)
			{
				GUI.changed = false;
				EditorUtility.SetDirty(_ctrl);
			}
		}
	}
}


//SkinsShopManagerEditor

using UnityEditor;
using UnityEngine;

namespace Figures
{
    [CustomEditor(typeof(SkinsShopManager))]
    public class SkinsShopManagerEditor : Editor
    {
        private SkinsShopManager _ctrl;

		private readonly string _boxType = "Box";
		private readonly string _removeLabel = "X";
		private readonly string _prevArrowLabel = "↑";
		private readonly string _nextArrowLabel = "↓";
		private readonly string _createNewSkinLabel = "Create new skin";

		#region Sort labels
		private readonly string _sortByPriceLabel = "Sort by price";
		private readonly string _sortByTypeLabel = "Sort by type";
		private readonly string _sortByTypeAndPriceLabel = "Sort by type and price";

		#endregion

		#region Skin labels
		private readonly string _typeSkinLabel = "Type:";
		private readonly string _isDefaultSkinLabel = "Default skin:";
		private readonly string _useCustomSkinColorLabel = "Use custom skin color?:";
		private readonly string _skinColorLabel = "New color?:";
		private readonly string _nameSkinLabel = "Name: ";
		private readonly string _priceSkinLabel = "Price: ";
		private readonly string _rightSkinLabel = "Right Sprite: ";
		private readonly string _leftSkinLabel = "Left Sprite: ";
		private readonly string _particleSkinLabel = "Particle skin: ";
		#endregion

		private void OnEnable()
        {
            _ctrl = target as SkinsShopManager;
		}

		public override void OnInspectorGUI()
        {
			DrawDefaultInspector();
            DrawCustomInspector();
        }

        private void DrawCustomInspector()
        {
            DrawSkins();
			SetEditorDirty();
		}

		private void SetEditorDirty()
		{
			if (GUI.changed)
			{
				GUI.changed = false;
				EditorUtility.SetDirty(_ctrl);
			}
		}

		private void DrawSkins()
        {
            EditorGUILayout.Space();
            EditorGUILayout.BeginVertical(_boxType);
            {
                EditorGUILayout.BeginHorizontal();
                {
                    GUILayout.FlexibleSpace();
                    if (GUILayout.Button(_sortByPriceLabel, GUILayout.Width(150), GUILayout.Height(20)))
                    {
                        _ctrl.Skins.Sort(delegate (Skin x, Skin y)
                        {
                            if (x.Price == 0) return 0;
                            else if (x.Price == 0) return -1;
                            else if (y.Price == 0) return 1;
                            else return x.Price.CompareTo(y.Price);
                        });
                    }
                    if (GUILayout.Button(_sortByTypeLabel, GUILayout.Width(150), GUILayout.Height(20)))
                    {
                        _ctrl.Skins.Sort(delegate (Skin x, Skin y)
                        {
                            return x.Type.CompareTo(y.Type);
                        });
                    }
                    if (GUILayout.Button(_sortByTypeAndPriceLabel, GUILayout.Width(150), GUILayout.Height(20)))
                    {
                        _ctrl.Skins.Sort(delegate (Skin x, Skin y)
                        {
                            if (x.Type != y.Type) return 0;
                            else return x.Price.CompareTo(y.Price);
                        });
                    }
                    GUILayout.FlexibleSpace();
                }
                EditorGUILayout.EndHorizontal();

				for (int i = 0; i < _ctrl.Skins.Count; i++)
				{
					Skin skin = _ctrl.Skins[i];

					switch (skin.Type)
					{
						case SkinType.MultipleSprites:
							DrawMultipleSpritesSkin(skin);
							break;
						case SkinType.Particles:
							DrawParticleSkinSkin(skin);
							break;
						case SkinType.SpritesWithParticles:
							DrawSpritesWithParticleSkin(skin);
							break;
					}
				}

				EditorGUILayout.BeginHorizontal();
                {
                    GUILayout.FlexibleSpace();
                    AddNewSkinButton();
                    GUILayout.FlexibleSpace();

                }
                EditorGUILayout.EndHorizontal();
            }
            EditorGUILayout.EndVertical();
        }

		#region Skin Types drawing
		private void DrawMultipleSpritesSkin(Skin skin)
        {
            EditorGUILayout.BeginVertical(_boxType);
            {
                EditorGUILayout.BeginHorizontal();
                {
                    EditorGUILayout.BeginVertical(_boxType, GUILayout.Height(200));
                    {
                        //TYPE
                        EditorGUILayout.BeginVertical();
                        {
                            EditorGUILayout.BeginHorizontal();
                            {
                                EditorGUILayout.LabelField(_typeSkinLabel, new GUIStyle() { alignment = TextAnchor.MiddleLeft, fontStyle = FontStyle.Bold, fontSize = 12, normal = new GUIStyleState() { textColor = Color.white } });
                            }
                            EditorGUILayout.EndHorizontal();
                            EditorGUILayout.BeginHorizontal();
                            {
                                skin.Type = (SkinType)EditorGUILayout.EnumPopup(skin.Type);
                            }
                            EditorGUILayout.EndHorizontal();
                        }
                        EditorGUILayout.EndVertical();

                        //NAME SPRITE
                        EditorGUILayout.BeginVertical();
                        {
                            EditorGUILayout.BeginHorizontal();
                            {
                                EditorGUILayout.LabelField(_nameSkinLabel, new GUIStyle() { alignment = TextAnchor.MiddleLeft, fontStyle = FontStyle.Bold, fontSize = 12, normal = new GUIStyleState() { textColor = Color.white } });
                            }
                            EditorGUILayout.EndHorizontal();
                            EditorGUILayout.BeginHorizontal();
                            {
                                skin.Name = EditorGUILayout.TextField(skin.Name);
                            }
                            EditorGUILayout.EndHorizontal();
                        }
                        EditorGUILayout.EndVertical();

                        //PRICE SPRITE
                        EditorGUILayout.BeginVertical();
                        {
                            EditorGUILayout.BeginHorizontal();
                            {
                                EditorGUILayout.LabelField(_priceSkinLabel, new GUIStyle() { alignment = TextAnchor.MiddleLeft, fontStyle = FontStyle.Bold, fontSize = 12, normal = new GUIStyleState() { textColor = Color.white } });
                            }
                            EditorGUILayout.EndHorizontal();
                            EditorGUILayout.BeginHorizontal();
                            {
                                skin.Price = EditorGUILayout.IntField(skin.Price);
                            }
                            EditorGUILayout.EndHorizontal();
                        }
                        EditorGUILayout.EndVertical();

                        //LEFT SPRITE

                        EditorGUILayout.BeginVertical();
                        {
                            EditorGUILayout.BeginHorizontal();
                            {
                                EditorGUILayout.LabelField(_leftSkinLabel, new GUIStyle() { alignment = TextAnchor.MiddleLeft, fontStyle = FontStyle.Bold, fontSize = 12, normal = new GUIStyleState() { textColor = Color.white } });
                            }
                            EditorGUILayout.EndHorizontal();
                            EditorGUILayout.BeginHorizontal();
                            {
                                skin.LeftSprite = (Sprite)EditorGUILayout.ObjectField(skin.LeftSprite, typeof(Sprite), true);
                            }
                            EditorGUILayout.EndHorizontal();
                        }
                        EditorGUILayout.EndVertical();

                        //RIGHT SPRITE
                        EditorGUILayout.BeginVertical();
                        {
                            EditorGUILayout.BeginHorizontal();
                            {
                                EditorGUILayout.LabelField(_rightSkinLabel, new GUIStyle() { alignment = TextAnchor.MiddleLeft, fontStyle = FontStyle.Bold, fontSize = 12, normal = new GUIStyleState() { textColor = Color.white } });
                            }
                            EditorGUILayout.EndHorizontal();
                            EditorGUILayout.BeginHorizontal();
                            {
                                skin.RightSprite = (Sprite)EditorGUILayout.ObjectField(skin.RightSprite, typeof(Sprite), true);
                            }
                            EditorGUILayout.EndHorizontal();
                        }
                        EditorGUILayout.EndVertical();

                        //USE CUSTOM COLOR FOR SKIN SPRITES BOOLEAN
                        EditorGUILayout.BeginVertical();
                        {
                            EditorGUILayout.BeginHorizontal();
                            {
                                EditorGUILayout.LabelField(_useCustomSkinColorLabel, new GUIStyle() { alignment = TextAnchor.MiddleLeft, fontStyle = FontStyle.Bold, fontSize = 12, normal = new GUIStyleState() { textColor = Color.white } });
                           
                                skin.UseCustomSpriteColor = EditorGUILayout.Toggle(skin.UseCustomSpriteColor);
                            }
                            EditorGUILayout.EndHorizontal();
                        }
                        EditorGUILayout.EndVertical();

                        if (skin.UseCustomSpriteColor)
                        {
                            EditorGUILayout.BeginVertical();
                            {
                                EditorGUILayout.BeginHorizontal();
                                {
                                    EditorGUILayout.LabelField(_skinColorLabel, new GUIStyle() { alignment = TextAnchor.MiddleLeft, fontStyle = FontStyle.Bold, fontSize = 12, normal = new GUIStyleState() { textColor = Color.white } });
                                }
                                EditorGUILayout.EndHorizontal();
                                EditorGUILayout.BeginHorizontal();
                                {
                                    skin.CustomSpriteColor = EditorGUILayout.ColorField(skin.CustomSpriteColor);
                                }
                                EditorGUILayout.EndHorizontal();
                            }
                            EditorGUILayout.EndVertical();
                        }

                        //IS DEFAULT SKIN BOOLEAN
                        EditorGUILayout.BeginVertical();
                        {
                            EditorGUILayout.BeginHorizontal();
                            {
                                EditorGUILayout.LabelField(_isDefaultSkinLabel, new GUIStyle() { alignment = TextAnchor.MiddleLeft, fontStyle = FontStyle.Bold, fontSize = 12, normal = new GUIStyleState() { textColor = Color.white } });
                            }
                            EditorGUILayout.EndHorizontal();
                            EditorGUILayout.BeginHorizontal();
                            {
                                skin.IsDefault = EditorGUILayout.Toggle(skin.IsDefault);
                            }
                            EditorGUILayout.EndHorizontal();
                        }
                        EditorGUILayout.EndVertical();

                    }
                    EditorGUILayout.EndVertical();

                    EditorGUILayout.BeginVertical(_boxType, GUILayout.MinWidth(200), GUILayout.Height(200));
                    {
                        GUILayout.FlexibleSpace();
                        EditorGUILayout.BeginHorizontal();
                        {
                            GUILayout.FlexibleSpace();
                            skin.ShopSkin = (Sprite)EditorGUILayout.ObjectField(skin.ShopSkin, typeof(Sprite), true, GUILayout.Width(150), GUILayout.Height(150));
                            GUILayout.FlexibleSpace();
                        }
                        EditorGUILayout.EndHorizontal();
                        GUILayout.FlexibleSpace();
                    }

                    EditorGUILayout.EndVertical();

					EditorGUILayout.BeginVertical();
					int index = _ctrl.Skins.IndexOf(skin);
					SetPrev(index);
					SetNext(index);
					RemovePlatformButton(skin);
					EditorGUILayout.EndVertical();
                }
                EditorGUILayout.EndHorizontal();
            }
            EditorGUILayout.EndVertical();
        }

        private void DrawParticleSkinSkin(Skin skin)
        {
            EditorGUILayout.BeginVertical(_boxType);
            {
                EditorGUILayout.BeginHorizontal();
                {
                    EditorGUILayout.BeginVertical(_boxType, GUILayout.Height(200));
                    {
                        //TYPE
                        EditorGUILayout.BeginVertical();
                        {
                            EditorGUILayout.BeginHorizontal();
                            {
                                EditorGUILayout.LabelField(_typeSkinLabel, new GUIStyle() { alignment = TextAnchor.MiddleLeft, fontStyle = FontStyle.Bold, fontSize = 12, normal = new GUIStyleState() { textColor = Color.white } });
                            }
                            EditorGUILayout.EndHorizontal();
                            EditorGUILayout.BeginHorizontal();
                            {
                                skin.Type = (SkinType)EditorGUILayout.EnumPopup(skin.Type);
                            }
                            EditorGUILayout.EndHorizontal();
                        }
                        EditorGUILayout.EndVertical();

                        //NAME SPRITE
                        EditorGUILayout.BeginVertical();
                        {
                            EditorGUILayout.BeginHorizontal();
                            {
                                EditorGUILayout.LabelField(_nameSkinLabel, new GUIStyle() { alignment = TextAnchor.MiddleLeft, fontStyle = FontStyle.Bold, fontSize = 12, normal = new GUIStyleState() { textColor = Color.white } });
                            }
                            EditorGUILayout.EndHorizontal();
                            EditorGUILayout.BeginHorizontal();
                            {
                                skin.Name = EditorGUILayout.TextField(skin.Name);
                            }
                            EditorGUILayout.EndHorizontal();
                        }
                        EditorGUILayout.EndVertical();

                        //PRICE SPRITE
                        EditorGUILayout.BeginVertical();
                        {
                            EditorGUILayout.BeginHorizontal();
                            {
                                EditorGUILayout.LabelField(_priceSkinLabel, new GUIStyle() { alignment = TextAnchor.MiddleLeft, fontStyle = FontStyle.Bold, fontSize = 12, normal = new GUIStyleState() { textColor = Color.white } });
                            }
                            EditorGUILayout.EndHorizontal();
                            EditorGUILayout.BeginHorizontal();
                            {
                                skin.Price = EditorGUILayout.IntField(skin.Price);
                            }
                            EditorGUILayout.EndHorizontal();
                        }
                        EditorGUILayout.EndVertical();

                        //PARTICLE OBJECT
                        EditorGUILayout.BeginVertical();
                        {
                            EditorGUILayout.BeginHorizontal();
                            {
                                EditorGUILayout.LabelField(_particleSkinLabel, new GUIStyle() { alignment = TextAnchor.MiddleLeft, fontStyle = FontStyle.Bold, fontSize = 12, normal = new GUIStyleState() { textColor = Color.white } });
                            }
                            EditorGUILayout.EndHorizontal();
                            EditorGUILayout.BeginHorizontal();
                            {
                                skin.ParticleSkin = (ParticleSystem)EditorGUILayout.ObjectField(skin.ParticleSkin, typeof(ParticleSystem), true);
                            }
                            EditorGUILayout.EndHorizontal();
                        }
                        EditorGUILayout.EndVertical();

						//IS DEFAULT SKIN BOOLEAN
						EditorGUILayout.BeginVertical();
						{
							EditorGUILayout.BeginHorizontal();
							{
								EditorGUILayout.LabelField(_isDefaultSkinLabel, new GUIStyle() { alignment = TextAnchor.MiddleLeft, fontStyle = FontStyle.Bold, fontSize = 12, normal = new GUIStyleState() { textColor = Color.white } });
							}
							EditorGUILayout.EndHorizontal();
							EditorGUILayout.BeginHorizontal();
							{
								skin.IsDefault = EditorGUILayout.Toggle(skin.IsDefault);
							}
							EditorGUILayout.EndHorizontal();
						}
						EditorGUILayout.EndVertical();

					}
                    EditorGUILayout.EndVertical();

                    EditorGUILayout.BeginVertical(_boxType, GUILayout.MinWidth(200), GUILayout.Height(200));
                    {
                        GUILayout.FlexibleSpace();
                        EditorGUILayout.BeginHorizontal();
                        {
                            GUILayout.FlexibleSpace();
                            skin.ShopSkin = (Sprite)EditorGUILayout.ObjectField(skin.ShopSkin, typeof(Sprite), true, GUILayout.Width(150), GUILayout.Height(150));
                            GUILayout.FlexibleSpace();
                        }
                        EditorGUILayout.EndHorizontal();
                        GUILayout.FlexibleSpace();
                    }

                    EditorGUILayout.EndVertical();

					EditorGUILayout.BeginVertical();
					int index = _ctrl.Skins.IndexOf(skin);
					SetPrev(index);
					SetNext(index);
					RemovePlatformButton(skin);
					EditorGUILayout.EndVertical();
				}
                EditorGUILayout.EndHorizontal();
            }
            EditorGUILayout.EndVertical();
        }

        private void DrawSpritesWithParticleSkin(Skin skin)
        {
            EditorGUILayout.BeginVertical(_boxType);
            {
                EditorGUILayout.BeginHorizontal();
                {
                    EditorGUILayout.BeginVertical(_boxType, GUILayout.Height(200));
                    {
                        //TYPE
                        EditorGUILayout.BeginVertical();
                        {
                            EditorGUILayout.BeginHorizontal();
                            {
                                EditorGUILayout.LabelField(_typeSkinLabel, new GUIStyle() { alignment = TextAnchor.MiddleLeft, fontStyle = FontStyle.Bold, fontSize = 12, normal = new GUIStyleState() { textColor = Color.white } });
                            }
                            EditorGUILayout.EndHorizontal();
                            EditorGUILayout.BeginHorizontal();
                            {
                                skin.Type = (SkinType)EditorGUILayout.EnumPopup(skin.Type);
                            }
                            EditorGUILayout.EndHorizontal();
                        }
                        EditorGUILayout.EndVertical();

                        //NAME SPRITE
                        EditorGUILayout.BeginVertical();
                        {
                            EditorGUILayout.BeginHorizontal();
                            {
                                EditorGUILayout.LabelField(_nameSkinLabel, new GUIStyle() { alignment = TextAnchor.MiddleLeft, fontStyle = FontStyle.Bold, fontSize = 12, normal = new GUIStyleState() { textColor = Color.white } });
                            }
                            EditorGUILayout.EndHorizontal();
                            EditorGUILayout.BeginHorizontal();
                            {
                                skin.Name = EditorGUILayout.TextField(skin.Name);
                            }
                            EditorGUILayout.EndHorizontal();
                        }
                        EditorGUILayout.EndVertical();

                        //PRICE SPRITE
                        EditorGUILayout.BeginVertical();
                        {
                            EditorGUILayout.BeginHorizontal();
                            {
                                EditorGUILayout.LabelField(_priceSkinLabel, new GUIStyle() { alignment = TextAnchor.MiddleLeft, fontStyle = FontStyle.Bold, fontSize = 12, normal = new GUIStyleState() { textColor = Color.white } });
                            }
                            EditorGUILayout.EndHorizontal();
                            EditorGUILayout.BeginHorizontal();
                            {
                                skin.Price = EditorGUILayout.IntField(skin.Price);
                            }
                            EditorGUILayout.EndHorizontal();
                        }
                        EditorGUILayout.EndVertical();

                        //LEFT SPRITE

                        EditorGUILayout.BeginVertical();
                        {
                            EditorGUILayout.BeginHorizontal();
                            {
                                EditorGUILayout.LabelField(_leftSkinLabel, new GUIStyle() { alignment = TextAnchor.MiddleLeft, fontStyle = FontStyle.Bold, fontSize = 12, normal = new GUIStyleState() { textColor = Color.white } });
                            }
                            EditorGUILayout.EndHorizontal();
                            EditorGUILayout.BeginHorizontal();
                            {
                                skin.LeftSprite = (Sprite)EditorGUILayout.ObjectField(skin.LeftSprite, typeof(Sprite), true);
                            }
                            EditorGUILayout.EndHorizontal();
                        }
                        EditorGUILayout.EndVertical();

                        //RIGHT SPRITE
                        EditorGUILayout.BeginVertical();
                        {
                            EditorGUILayout.BeginHorizontal();
                            {
                                EditorGUILayout.LabelField(_rightSkinLabel, new GUIStyle() { alignment = TextAnchor.MiddleLeft, fontStyle = FontStyle.Bold, fontSize = 12, normal = new GUIStyleState() { textColor = Color.white } });
                            }
                            EditorGUILayout.EndHorizontal();
                            EditorGUILayout.BeginHorizontal();
                            {
                                skin.RightSprite = (Sprite)EditorGUILayout.ObjectField(skin.RightSprite, typeof(Sprite), true);
                            }
                            EditorGUILayout.EndHorizontal();
                        }
                        EditorGUILayout.EndVertical();

                        //USE CUSTOM COLOR FOR SKIN SPRITES BOOLEAN
                        EditorGUILayout.BeginVertical();
                        {
                            EditorGUILayout.BeginHorizontal();
                            {
                                EditorGUILayout.LabelField(_useCustomSkinColorLabel, new GUIStyle() { alignment = TextAnchor.MiddleLeft, fontStyle = FontStyle.Bold, fontSize = 12, normal = new GUIStyleState() { textColor = Color.white } });
                                
                                skin.UseCustomSpriteColor = EditorGUILayout.Toggle(skin.UseCustomSpriteColor);
                            }
                            EditorGUILayout.EndHorizontal();
                        }
                        EditorGUILayout.EndVertical();

                        if (skin.UseCustomSpriteColor)
                        {
                            EditorGUILayout.BeginVertical();
                            {
                                EditorGUILayout.BeginHorizontal();
                                {
                                    EditorGUILayout.LabelField(_skinColorLabel, new GUIStyle() { alignment = TextAnchor.MiddleLeft, fontStyle = FontStyle.Bold, fontSize = 12, normal = new GUIStyleState() { textColor = Color.white } });
                                }
                                EditorGUILayout.EndHorizontal();
                                EditorGUILayout.BeginHorizontal();
                                {
                                    skin.CustomSpriteColor = EditorGUILayout.ColorField(skin.CustomSpriteColor);
                                }
                                EditorGUILayout.EndHorizontal();
                            }
                            EditorGUILayout.EndVertical();
                        }


                        //PARTICLE OBJECT
                        EditorGUILayout.BeginVertical();
                        {
                            EditorGUILayout.BeginHorizontal();
                            {
                                EditorGUILayout.LabelField(_particleSkinLabel, new GUIStyle() { alignment = TextAnchor.MiddleLeft, fontStyle = FontStyle.Bold, fontSize = 12, normal = new GUIStyleState() { textColor = Color.white } });
                            }
                            EditorGUILayout.EndHorizontal();
                            EditorGUILayout.BeginHorizontal();
                            {
                                skin.ParticleSkin = (ParticleSystem)EditorGUILayout.ObjectField(skin.ParticleSkin, typeof(ParticleSystem), true);
                            }
                            EditorGUILayout.EndHorizontal();
                        }
                        EditorGUILayout.EndVertical();

						//IS DEFAULT SKIN BOOLEAN
						EditorGUILayout.BeginVertical();
						{
							EditorGUILayout.BeginHorizontal();
							{
								EditorGUILayout.LabelField(_isDefaultSkinLabel, new GUIStyle() { alignment = TextAnchor.MiddleLeft, fontStyle = FontStyle.Bold, fontSize = 12, normal = new GUIStyleState() { textColor = Color.white } });
							}
							EditorGUILayout.EndHorizontal();
							EditorGUILayout.BeginHorizontal();
							{
								skin.IsDefault = EditorGUILayout.Toggle(skin.IsDefault);
							}
							EditorGUILayout.EndHorizontal();
						}
						EditorGUILayout.EndVertical();
					}
                    EditorGUILayout.EndVertical();

                    EditorGUILayout.BeginVertical(_boxType, GUILayout.MinWidth(200), GUILayout.Height(200));
                    {
                        GUILayout.FlexibleSpace();
                        EditorGUILayout.BeginHorizontal();
                        {
                            GUILayout.FlexibleSpace();
                            skin.ShopSkin = (Sprite)EditorGUILayout.ObjectField(skin.ShopSkin, typeof(Sprite), true, GUILayout.Width(150), GUILayout.Height(150));
                            GUILayout.FlexibleSpace();
                        }
                        EditorGUILayout.EndHorizontal();
                        GUILayout.FlexibleSpace();
                    }

                    EditorGUILayout.EndVertical();

					EditorGUILayout.BeginVertical();
					int index = _ctrl.Skins.IndexOf(skin);
					SetPrev(index);
					SetNext(index);
					RemovePlatformButton(skin);
					EditorGUILayout.EndVertical();
				}
                EditorGUILayout.EndHorizontal();
            }
            EditorGUILayout.EndVertical();
        }
		#endregion

		#region Manipulation with skin item in list
		private void AddNewSkinButton()
        {
			GUI.backgroundColor = Color.green;
			if (GUILayout.Button(_createNewSkinLabel, GUILayout.Width(150), GUILayout.Height(20)))
            {
                _ctrl.Skins.Add(new Skin());
            }
			GUI.backgroundColor = Color.white;
		}

		private void RemovePlatformButton(Skin skin)
        {
			GUI.backgroundColor = Color.red;
			if (GUILayout.Button(_removeLabel, GUILayout.Width(23), GUILayout.Height(20)))
            {
                if (_ctrl.Skins.Contains(skin))
                {
                    _ctrl.Skins.Remove(skin);
                }
            }
			GUI.backgroundColor = Color.white;
		}

		/// <summary>
		/// Change shop item position in list. Set current index position - 1.
		/// </summary>
		/// <param name="index"> Index of element. </param>
		private void SetPrev(int index)
		{
			if (index == 0)
				return;

			GUI.backgroundColor = Color.yellow;
			if (GUILayout.Button(_prevArrowLabel, GUILayout.Width(23), GUILayout.Height(20)))
			{
				Skin current = _ctrl.Skins[index];
				Skin prev = _ctrl.Skins[index - 1];

				_ctrl.Skins[index] = prev;
				_ctrl.Skins[index - 1] = current;
			}
			GUI.backgroundColor = Color.white;
		}

		/// <summary>
		/// Change shop item position in list. Set current index position + 1.
		/// </summary>
		/// <param name="index"> Index of element. </param>
		private void SetNext(int index)
		{
			if (index == (_ctrl.Skins.Count - 1))
				return;
			GUI.backgroundColor = Color.yellow;
			if (GUILayout.Button(_nextArrowLabel, GUILayout.Width(23), GUILayout.Height(20)))
			{
				Skin current = _ctrl.Skins[index];
				Skin next = _ctrl.Skins[index + 1];

				_ctrl.Skins[index] = next;
				_ctrl.Skins[index + 1] = current;
			}
			GUI.backgroundColor = Color.white;
		}
		#endregion
	}
}

//FreeCurrencyType

namespace Figures
{
    public enum FreeCurencyType
	{
        Hourly = 0,
        Daily
    }
}