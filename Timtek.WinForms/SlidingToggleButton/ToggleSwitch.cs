using System.ComponentModel;
using Timtek.WinForms.SlidingToggleButton.Renderers;
using Timer = System.Windows.Forms.Timer;

/**********************************************************************************/
/*                          ToggleSwitch - Version  1.1                           */
/**********************************************************************************/
/*   http://www.codeproject.com/Articles/1029499/ToggleSwitch-Winforms-Control    */
/**********************************************************************************/


namespace Timtek.WinForms.SlidingToggleButton;

[DefaultValue("Checked")]
[DefaultEvent("CheckedChanged")]
[ToolboxBitmap(typeof(CheckBox))]
public class ToggleSwitch : Control
{
    #region Delegate and Event declarations

    public delegate void CheckedChangedDelegate(object sender, EventArgs e);

    [Description("Raised when the ToggleSwitch has changed state")]
    public event CheckedChangedDelegate CheckedChanged;

    public delegate void BeforeRenderingDelegate(object sender, BeforeRenderingEventArgs e);

    [Description("Raised when the ToggleSwitch renderer is changed")]
    public event BeforeRenderingDelegate BeforeRendering;

    #endregion Delegate and Event declarations

    #region Enums

    public enum ToggleSwitchStyle
    {
        Metro,
        Android,
        IOS5,
        BrushedMetal,
        OSX,
        Carbon,
        Iphone,
        Fancy,
        Modern,
        PlainAndSimpel
    }

    public enum ToggleSwitchAlignment
    {
        Near,
        Center,
        Far
    }

    public enum ToggleSwitchButtonAlignment
    {
        Left,
        Center,
        Right
    }

    #endregion Enums

    #region Private Members

    private readonly Timer _animationTimer = new();
    private ToggleSwitchRendererBase _renderer;

    private ToggleSwitchStyle _style = ToggleSwitchStyle.Metro;
    private bool _checked;
    private bool _moving;
    private bool _animating;
    private int _animationTarget;
    private int _animationInterval = 1;
    private int _animationStep = 10;

    private bool _isLeftFieldHovered;
    private bool _isButtonHovered;
    private bool _isRightFieldHovered;

    private int _buttonValue;
    private int _savedButtonValue;
    private int _xOffset;
    private int _xValue;
    private bool _grayWhenDisabled = true;

    private MouseEventArgs? _lastMouseEventArgs;

    private bool _buttonScaleImage;
    private ToggleSwitchButtonAlignment _buttonAlignment = ToggleSwitchButtonAlignment.Center;
    private Image _buttonImage;

    private string _offText = "";
    private Color _offForeColor = Color.Black;
    private Font _offFont;
    private Image _offSideImage;
    private bool _offSideScaleImage;
    private ToggleSwitchAlignment _offSideAlignment = ToggleSwitchAlignment.Center;
    private Image _offButtonImage;
    private bool _offButtonScaleImage;
    private ToggleSwitchButtonAlignment _offButtonAlignment = ToggleSwitchButtonAlignment.Center;

    private string _onText = "";
    private Color _onForeColor = Color.Black;
    private Font _onFont;
    private Image _onSideImage;
    private bool _onSideScaleImage;
    private ToggleSwitchAlignment _onSideAlignment = ToggleSwitchAlignment.Center;
    private Image _onButtonImage;
    private bool _onButtonScaleImage;
    private ToggleSwitchButtonAlignment _onButtonAlignment = ToggleSwitchButtonAlignment.Center;

    #endregion Private Members

    #region Constructor Etc.

    public ToggleSwitch()
    {
        SetStyle(ControlStyles.ResizeRedraw |
                 ControlStyles.SupportsTransparentBackColor |
                 ControlStyles.AllPaintingInWmPaint |
                 ControlStyles.UserPaint |
                 ControlStyles.OptimizedDoubleBuffer |
                 ControlStyles.DoubleBuffer, true);

        OnFont = base.Font;
        OffFont = base.Font;

        SetRenderer(new ToggleSwitchMetroRenderer());

        _animationTimer.Enabled = false;
        _animationTimer.Interval = _animationInterval;
        _animationTimer.Tick += AnimationTimer_Tick;
    }

    public void SetRenderer(ToggleSwitchRendererBase renderer)
    {
        renderer.SetToggleSwitch(this);
        _renderer = renderer;

        if (_renderer != null)
            Refresh();
    }

    #endregion Constructor Etc.

    #region Public Properties

    [Bindable(false)]
    [DefaultValue(typeof(ToggleSwitchStyle), "Metro")]
    [Category("Appearance")]
    [Description("Gets or sets the style of the ToggleSwitch")]
    public ToggleSwitchStyle Style
    {
        get => _style;
        set
        {
            if (value != _style)
            {
                _style = value;

                switch (_style)
                {
                    case ToggleSwitchStyle.Metro:
                        SetRenderer(new ToggleSwitchMetroRenderer());
                        break;
                    case ToggleSwitchStyle.Android:
                        SetRenderer(new ToggleSwitchAndroidRenderer());
                        break;
                    case ToggleSwitchStyle.IOS5:
                        SetRenderer(new ToggleSwitchIOS5Renderer());
                        break;
                    case ToggleSwitchStyle.BrushedMetal:
                        SetRenderer(new ToggleSwitchBrushedMetalRenderer());
                        break;
                    case ToggleSwitchStyle.OSX:
                        SetRenderer(new ToggleSwitchOSXRenderer());
                        break;
                    case ToggleSwitchStyle.Carbon:
                        SetRenderer(new ToggleSwitchCarbonRenderer());
                        break;
                    case ToggleSwitchStyle.Iphone:
                        SetRenderer(new ToggleSwitchIphoneRenderer());
                        break;
                    case ToggleSwitchStyle.Fancy:
                        SetRenderer(new ToggleSwitchFancyRenderer());
                        break;
                    case ToggleSwitchStyle.Modern:
                        SetRenderer(new ToggleSwitchModernRenderer());
                        break;
                    case ToggleSwitchStyle.PlainAndSimpel:
                        SetRenderer(new ToggleSwitchPlainAndSimpleRenderer());
                        break;
                }
            }

            Refresh();
        }
    }

    [Bindable(true)]
    [DefaultValue(false)]
    [Category("Data")]
    [Description("Gets or sets the Checked value of the ToggleSwitch")]
    public bool Checked
    {
        get => _checked;
        set
        {
            if (value != _checked)
            {
                while (_animating) Application.DoEvents();

                if (value)
                {
                    var buttonWidth = _renderer.GetButtonWidth();
                    _animationTarget = Width - buttonWidth;
                    BeginAnimation(true);
                }
                else
                {
                    _animationTarget = 0;
                    BeginAnimation(false);
                }
            }
        }
    }

    [Bindable(true)]
    [DefaultValue(true)]
    [Category("Behavior")]
    [Description("Gets or sets whether the user can change the value of the button or not")]
    public bool AllowUserChange { get; set; } = true;

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string CheckedString =>
        Checked ? string.IsNullOrEmpty(OnText) ? "ON" : OnText : string.IsNullOrEmpty(OffText) ? "OFF" : OffText;

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Rectangle ButtonRectangle => _renderer.GetButtonRectangle();

    [Bindable(false)]
    [DefaultValue(true)]
    [Category("Appearance")]
    [Description("Gets or sets if the ToggleSwitch should be grayed out when disabled")]
    public bool GrayWhenDisabled
    {
        get => _grayWhenDisabled;
        set
        {
            if (value != _grayWhenDisabled)
            {
                _grayWhenDisabled = value;

                if (!Enabled)
                    Refresh();
            }
        }
    }

    [Bindable(false)]
    [DefaultValue(true)]
    [Category("Behavior")]
    [Description("Gets or sets if the ToggleSwitch should toggle when the button is clicked")]
    public bool ToggleOnButtonClick { get; set; } = true;

    [Bindable(false)]
    [DefaultValue(true)]
    [Category("Behavior")]
    [Description("Gets or sets if the ToggleSwitch should toggle when the track besides the button is clicked")]
    public bool ToggleOnSideClick { get; set; } = true;

    [Bindable(false)]
    [DefaultValue(50)]
    [Category("Behavior")]
    [Description("Gets or sets how much the button need to be on the other side (in peercept) before it snaps")]
    public int ThresholdPercentage { get; set; } = 50;

    [Bindable(false)]
    [DefaultValue(typeof(Color), "Black")]
    [Category("Appearance")]
    [Description("Gets or sets the forecolor of the text when Checked=false")]
    public Color OffForeColor
    {
        get => _offForeColor;
        set
        {
            if (value != _offForeColor)
            {
                _offForeColor = value;
                Refresh();
            }
        }
    }

    [Bindable(false)]
    [DefaultValue(typeof(Font), "Microsoft Sans Serif, 8.25pt")]
    [Category("Appearance")]
    [Description("Gets or sets the font of the text when Checked=false")]
    public Font OffFont
    {
        get => _offFont;
        set
        {
            if (!value.Equals(_offFont))
            {
                _offFont = value;
                Refresh();
            }
        }
    }

    [Bindable(false)]
    [DefaultValue("")]
    [Category("Appearance")]
    [Description("Gets or sets the text when Checked=true")]
    public string OffText
    {
        get => _offText;
        set
        {
            if (value != _offText)
            {
                _offText = value;
                Refresh();
            }
        }
    }

    [Bindable(false)]
    [DefaultValue(null)]
    [Category("Appearance")]
    [Description(
        "Gets or sets the side image when Checked=false - Note: Settings the OffSideImage overrules the OffText property. Only the image will be shown")]
    public Image OffSideImage
    {
        get => _offSideImage;
        set
        {
            if (value != _offSideImage)
            {
                _offSideImage = value;
                Refresh();
            }
        }
    }

    [Bindable(false)]
    [DefaultValue(false)]
    [Category("Behavior")]
    [Description("Gets or sets whether the side image visible when Checked=false should be scaled to fit")]
    public bool OffSideScaleImageToFit
    {
        get => _offSideScaleImage;
        set
        {
            if (value != _offSideScaleImage)
            {
                _offSideScaleImage = value;
                Refresh();
            }
        }
    }

    [Bindable(false)]
    [DefaultValue(typeof(ToggleSwitchAlignment), "Center")]
    [Category("Appearance")]
    [Description("Gets or sets how the text or side image visible when Checked=false should be aligned")]
    public ToggleSwitchAlignment OffSideAlignment
    {
        get => _offSideAlignment;
        set
        {
            if (value != _offSideAlignment)
            {
                _offSideAlignment = value;
                Refresh();
            }
        }
    }

    [Bindable(false)]
    [DefaultValue(null)]
    [Category("Appearance")]
    [Description("Gets or sets the button image when Checked=false and ButtonImage is not set")]
    public Image OffButtonImage
    {
        get => _offButtonImage;
        set
        {
            if (value != _offButtonImage)
            {
                _offButtonImage = value;
                Refresh();
            }
        }
    }

    [Bindable(false)]
    [DefaultValue(false)]
    [Category("Behavior")]
    [Description("Gets or sets whether the button image visible when Checked=false should be scaled to fit")]
    public bool OffButtonScaleImageToFit
    {
        get => _offButtonScaleImage;
        set
        {
            if (value != _offButtonScaleImage)
            {
                _offButtonScaleImage = value;
                Refresh();
            }
        }
    }

    [Bindable(false)]
    [DefaultValue(typeof(ToggleSwitchButtonAlignment), "Center")]
    [Category("Appearance")]
    [Description("Gets or sets how the button image visible when Checked=false should be aligned")]
    public ToggleSwitchButtonAlignment OffButtonAlignment
    {
        get => _offButtonAlignment;
        set
        {
            if (value != _offButtonAlignment)
            {
                _offButtonAlignment = value;
                Refresh();
            }
        }
    }

    [Bindable(false)]
    [DefaultValue(typeof(Color), "Black")]
    [Category("Appearance")]
    [Description("Gets or sets the forecolor of the text when Checked=true")]
    public Color OnForeColor
    {
        get => _onForeColor;
        set
        {
            if (value != _onForeColor)
            {
                _onForeColor = value;
                Refresh();
            }
        }
    }

    [Bindable(false)]
    [DefaultValue(typeof(Font), "Microsoft Sans Serif, 8,25pt")]
    [Category("Appearance")]
    [Description("Gets or sets the font of the text when Checked=true")]
    public Font OnFont
    {
        get => _onFont;
        set
        {
            if (!value.Equals(_onFont))
            {
                _onFont = value;
                Refresh();
            }
        }
    }

    [Bindable(false)]
    [DefaultValue("")]
    [Category("Appearance")]
    [Description("Gets or sets the text when Checked=true")]
    public string OnText
    {
        get => _onText;
        set
        {
            if (value != _onText)
            {
                _onText = value;
                Refresh();
            }
        }
    }

    [Bindable(false)]
    [DefaultValue(null)]
    [Category("Appearance")]
    [Description(
        "Gets or sets the side image visible when Checked=true - Note: Settings the OnSideImage overrules the OnText property. Only the image will be shown.")]
    public Image OnSideImage
    {
        get => _onSideImage;
        set
        {
            if (value != _onSideImage)
            {
                _onSideImage = value;
                Refresh();
            }
        }
    }

    [Bindable(false)]
    [DefaultValue(false)]
    [Category("Behavior")]
    [Description("Gets or sets whether the side image visible when Checked=true should be scaled to fit")]
    public bool OnSideScaleImageToFit
    {
        get => _onSideScaleImage;
        set
        {
            if (value != _onSideScaleImage)
            {
                _onSideScaleImage = value;
                Refresh();
            }
        }
    }

    [Bindable(false)]
    [DefaultValue(null)]
    [Category("Appearance")]
    [Description("Gets or sets the button image")]
    public Image ButtonImage
    {
        get => _buttonImage;
        set
        {
            if (value != _buttonImage)
            {
                _buttonImage = value;
                Refresh();
            }
        }
    }

    [Bindable(false)]
    [DefaultValue(false)]
    [Category("Behavior")]
    [Description("Gets or sets whether the button image should be scaled to fit")]
    public bool ButtonScaleImageToFit
    {
        get => _buttonScaleImage;
        set
        {
            if (value != _buttonScaleImage)
            {
                _buttonScaleImage = value;
                Refresh();
            }
        }
    }

    [Bindable(false)]
    [DefaultValue(typeof(ToggleSwitchButtonAlignment), "Center")]
    [Category("Appearance")]
    [Description("Gets or sets how the button image should be aligned")]
    public ToggleSwitchButtonAlignment ButtonAlignment
    {
        get => _buttonAlignment;
        set
        {
            if (value != _buttonAlignment)
            {
                _buttonAlignment = value;
                Refresh();
            }
        }
    }

    [Bindable(false)]
    [DefaultValue(typeof(ToggleSwitchAlignment), "Center")]
    [Category("Appearance")]
    [Description("Gets or sets how the text or side image visible when Checked=true should be aligned")]
    public ToggleSwitchAlignment OnSideAlignment
    {
        get => _onSideAlignment;
        set
        {
            if (value != _onSideAlignment)
            {
                _onSideAlignment = value;
                Refresh();
            }
        }
    }

    [Bindable(false)]
    [DefaultValue(null)]
    [Category("Appearance")]
    [Description("Gets or sets the button image visible when Checked=true and ButtonImage is not set")]
    public Image OnButtonImage
    {
        get => _onButtonImage;
        set
        {
            if (value != _onButtonImage)
            {
                _onButtonImage = value;
                Refresh();
            }
        }
    }

    [Bindable(false)]
    [DefaultValue(false)]
    [Category("Behavior")]
    [Description("Gets or sets whether the button image visible when Checked=true should be scaled to fit")]
    public bool OnButtonScaleImageToFit
    {
        get => _onButtonScaleImage;
        set
        {
            if (value != _onButtonScaleImage)
            {
                _onButtonScaleImage = value;
                Refresh();
            }
        }
    }

    [Bindable(false)]
    [DefaultValue(typeof(ToggleSwitchButtonAlignment), "Center")]
    [Category("Appearance")]
    [Description("Gets or sets how the button image visible when Checked=true should be aligned")]
    public ToggleSwitchButtonAlignment OnButtonAlignment
    {
        get => _onButtonAlignment;
        set
        {
            if (value != _onButtonAlignment)
            {
                _onButtonAlignment = value;
                Refresh();
            }
        }
    }

    [Bindable(false)]
    [DefaultValue(true)]
    [Category("Behavior")]
    [Description("Gets or sets whether the toggle change should be animated or not")]
    public bool UseAnimation { get; set; } = true;

    [Bindable(false)]
    [DefaultValue(1)]
    [Category("Behavior")]
    [Description("Gets or sets the interval in ms between animation frames")]
    public int AnimationInterval
    {
        get => _animationInterval;
        set
        {
            if (value <= 0) throw new ArgumentOutOfRangeException("AnimationInterval must larger than zero!");

            _animationInterval = value;
        }
    }

    [Bindable(false)]
    [DefaultValue(10)]
    [Category("Behavior")]
    [Description("Gets or sets the step in pixes the button shouldbe moved between each animation interval")]
    public int AnimationStep
    {
        get => _animationStep;
        set
        {
            if (value <= 0) throw new ArgumentOutOfRangeException("AnimationStep must larger than zero!");

            _animationStep = value;
        }
    }

    #region Hidden Base Properties

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new string Text
    {
        get => "";
        set => base.Text = "";
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new Color ForeColor
    {
        get => Color.Black;
        set => base.ForeColor = Color.Black;
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new Font Font
    {
        get => base.Font;
        set => base.Font = new Font(base.Font, FontStyle.Regular);
    }

    #endregion Hidden Base Properties

    #endregion Public Properties

    #region Internal Properties

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    internal bool IsButtonHovered => _isButtonHovered && !IsButtonPressed;

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    internal bool IsButtonPressed { get; private set; }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    internal bool IsLeftSideHovered => _isLeftFieldHovered && !IsLeftSidePressed;

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    internal bool IsLeftSidePressed { get; private set; }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    internal bool IsRightSideHovered => _isRightFieldHovered && !IsRightSidePressed;

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    internal bool IsRightSidePressed { get; private set; }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    internal int ButtonValue
    {
        get
        {
            if (_animating || _moving)
                return _buttonValue;
            if (_checked)
                return Width - _renderer.GetButtonWidth();
            return 0;
        }
        set
        {
            if (value != _buttonValue)
            {
                _buttonValue = value;
                Refresh();
            }
        }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    internal bool IsButtonOnLeftSide => ButtonValue <= 0;

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    internal bool IsButtonOnRightSide => ButtonValue >= Width - _renderer.GetButtonWidth();

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    internal bool IsButtonMovingLeft => _animating && !IsButtonOnLeftSide && AnimationResult == false;

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    internal bool IsButtonMovingRight => _animating && !IsButtonOnRightSide && AnimationResult;

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    internal bool AnimationResult { get; private set; }

    #endregion Private Properties

    #region Overridden Control Methods

    protected override Size DefaultSize => new(50, 19);

    protected override void OnPaintBackground(PaintEventArgs pevent)
    {
        pevent.Graphics.ResetClip();

        base.OnPaintBackground(pevent);

        if (_renderer != null)
            _renderer.RenderBackground(pevent);
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        e.Graphics.ResetClip();

        base.OnPaint(e);

        if (_renderer != null)
        {
            if (BeforeRendering != null) BeforeRendering(this, new BeforeRenderingEventArgs(_renderer));

            _renderer.RenderControl(e);
        }
    }

    protected override void OnMouseMove(MouseEventArgs e)
    {
        _lastMouseEventArgs = e;

        var buttonWidth = _renderer.GetButtonWidth();
        var buttonRectangle = _renderer.GetButtonRectangle(buttonWidth);

        if (_moving)
        {
            var val = _xValue + (e.Location.X - _xOffset);

            if (val < 0)
                val = 0;

            if (val > Width - buttonWidth)
                val = Width - buttonWidth;

            ButtonValue = val;
            Refresh();
            return;
        }

        if (buttonRectangle.Contains(e.Location))
        {
            _isButtonHovered = true;
            _isLeftFieldHovered = false;
            _isRightFieldHovered = false;
        }
        else
        {
            if (e.Location.X > buttonRectangle.X + buttonRectangle.Width)
            {
                _isButtonHovered = false;
                _isLeftFieldHovered = false;
                _isRightFieldHovered = true;
            }
            else if (e.Location.X < buttonRectangle.X)
            {
                _isButtonHovered = false;
                _isLeftFieldHovered = true;
                _isRightFieldHovered = false;
            }
        }

        Refresh();
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
        if (_animating || !AllowUserChange)
            return;

        var buttonWidth = _renderer.GetButtonWidth();
        var buttonRectangle = _renderer.GetButtonRectangle(buttonWidth);

        _savedButtonValue = ButtonValue;

        if (buttonRectangle.Contains(e.Location))
        {
            IsButtonPressed = true;
            IsLeftSidePressed = false;
            IsRightSidePressed = false;

            _moving = true;
            _xOffset = e.Location.X;
            _buttonValue = buttonRectangle.X;
            _xValue = ButtonValue;
        }
        else
        {
            if (e.Location.X > buttonRectangle.X + buttonRectangle.Width)
            {
                IsButtonPressed = false;
                IsLeftSidePressed = false;
                IsRightSidePressed = true;
            }
            else if (e.Location.X < buttonRectangle.X)
            {
                IsButtonPressed = false;
                IsLeftSidePressed = true;
                IsRightSidePressed = false;
            }
        }

        Refresh();
    }

    protected override void OnMouseUp(MouseEventArgs e)
    {
        if (_animating || !AllowUserChange)
            return;

        var buttonWidth = _renderer.GetButtonWidth();

        var wasLeftSidePressed = IsLeftSidePressed;
        var wasRightSidePressed = IsRightSidePressed;

        IsButtonPressed = false;
        IsLeftSidePressed = false;
        IsRightSidePressed = false;

        if (_moving)
        {
            var percentage = (int)(100 * (double)ButtonValue / (Width - (double)buttonWidth));

            if (_checked)
            {
                if (percentage <= 100 - ThresholdPercentage)
                {
                    _animationTarget = 0;
                    BeginAnimation(false);
                }
                else if (ToggleOnButtonClick && _savedButtonValue == ButtonValue)
                {
                    _animationTarget = 0;
                    BeginAnimation(false);
                }
                else
                {
                    _animationTarget = Width - buttonWidth;
                    BeginAnimation(true);
                }
            }
            else
            {
                if (percentage >= ThresholdPercentage)
                {
                    _animationTarget = Width - buttonWidth;
                    BeginAnimation(true);
                }
                else if (ToggleOnButtonClick && _savedButtonValue == ButtonValue)
                {
                    _animationTarget = Width - buttonWidth;
                    BeginAnimation(true);
                }
                else
                {
                    _animationTarget = 0;
                    BeginAnimation(false);
                }
            }

            _moving = false;
            return;
        }

        if (IsButtonOnRightSide)
        {
            _buttonValue = Width - buttonWidth;
            _animationTarget = 0;
        }
        else
        {
            _buttonValue = 0;
            _animationTarget = Width - buttonWidth;
        }

        if (wasLeftSidePressed && ToggleOnSideClick)
            SetValueInternal(false);
        else if (wasRightSidePressed && ToggleOnSideClick) SetValueInternal(true);

        Refresh();
    }

    protected override void OnMouseLeave(EventArgs e)
    {
        _isButtonHovered = false;
        _isLeftFieldHovered = false;
        _isRightFieldHovered = false;
        IsButtonPressed = false;
        IsLeftSidePressed = false;
        IsRightSidePressed = false;

        Refresh();
    }

    protected override void OnEnabledChanged(EventArgs e)
    {
        base.OnEnabledChanged(e);
        Refresh();
    }

    protected override void OnRegionChanged(EventArgs e)
    {
        base.OnRegionChanged(e);
        Refresh();
    }

    protected override void OnSizeChanged(EventArgs e)
    {
        if (_animationTarget > 0)
        {
            var buttonWidth = _renderer.GetButtonWidth();
            _animationTarget = Width - buttonWidth;
        }

        base.OnSizeChanged(e);
    }

    #endregion Overridden Control Methods

    #region Private Methods

    private void SetValueInternal(bool checkedValue)
    {
        if (checkedValue == _checked)
            return;

        while (_animating) Application.DoEvents();

        BeginAnimation(checkedValue);
    }

    private void BeginAnimation(bool checkedValue)
    {
        _animating = true;
        AnimationResult = checkedValue;

        if (_animationTimer != null && UseAnimation)
        {
            _animationTimer.Interval = _animationInterval;
            _animationTimer.Enabled = true;
        }
        else
        {
            AnimationComplete();
        }
    }

    private void AnimationTimer_Tick(object sender, EventArgs e)
    {
        _animationTimer.Enabled = false;

        var animationDone = false;
        int newButtonValue;

        if (IsButtonMovingRight)
        {
            newButtonValue = ButtonValue + _animationStep;

            if (newButtonValue > _animationTarget)
                newButtonValue = _animationTarget;

            ButtonValue = newButtonValue;

            animationDone = ButtonValue >= _animationTarget;
        }
        else
        {
            newButtonValue = ButtonValue - _animationStep;

            if (newButtonValue < _animationTarget)
                newButtonValue = _animationTarget;

            ButtonValue = newButtonValue;

            animationDone = ButtonValue <= _animationTarget;
        }

        if (animationDone)
            AnimationComplete();
        else
            _animationTimer.Enabled = true;
    }

    private void AnimationComplete()
    {
        _animating = false;
        _moving = false;
        _checked = AnimationResult;

        _isButtonHovered = false;
        IsButtonPressed = false;
        _isLeftFieldHovered = false;
        IsLeftSidePressed = false;
        _isRightFieldHovered = false;
        IsRightSidePressed = false;

        Refresh();

        if (CheckedChanged != null)
            CheckedChanged(this, new EventArgs());

        if (_lastMouseEventArgs != null)
            OnMouseMove(_lastMouseEventArgs);

        _lastMouseEventArgs = null;
    }

    #endregion Private Methods
}