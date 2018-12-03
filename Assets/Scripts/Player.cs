using Assets.Scripts.Managers;
using Assets.Scripts.Signals;
using Assets.Scripts.UI.Inventory;
using UnityEngine;
using Zenject;

namespace Assets.Scripts
{
    public class Player : MonoBehaviour
    {
        public Sprite playerSprite;
        public Sprite carSprite;

        public LineRenderer Road;
        public float CarSpeed = 15f;
        public float PlayerSpeed = 5f;
        public float FuelBurnRate = 0.1f;

        private SignalBus _signalBus;
        private SpriteRenderer _playerSprite;
        private int _currentRoadNode = 1;
        private Rigidbody2D _rigidbody2D;

        private DirectionType _direction = DirectionType.East;
        private bool _looting = false;

        private Car.Factory _carFactory;

        private bool _inCar = true;

        public float Car = 0.1f;
        public float Fuel = 0.1f;

        private AudioSource _engineSound;

        private float _enginePitch = 2.0f;

        [Inject]
        private void Init(SignalBus signalBus, Car.Factory carFactory)
        {
            _signalBus = signalBus;
            _carFactory = carFactory;

            _signalBus.Subscribe<ChangeDirectionSignal>((signal) =>
            {
                if (_direction != signal.Direction)
                {
                    IncrementRoadNode(signal.Direction);
                }
                _direction = signal.Direction;

                _looting = false;

                _engineSound.Play();
                _enginePitch = 2.0f;
                


                _signalBus.Fire(new UIHideSignal(UIPanelType.Loot));
            });

            _signalBus.Subscribe<UIShowSignal>((signal) =>
            {
                if (signal.PanelType == UIPanelType.Loot)
                {
                    _looting = true;
                    _enginePitch = 1.0f;
                }
            });


            // get in and out of car signals
            _signalBus.Subscribe<GetOutCarSignal>((signal) =>
            {
                var car = _carFactory.Create();
                car.transform.position = this.transform.position;
                _playerSprite.sprite = playerSprite;
                _inCar = false;

                _engineSound.Stop();
            });

            _signalBus.Subscribe<GetInCarSignal>((signal) =>
            {
                _playerSprite.sprite = carSprite;
                _inCar = true;

                _engineSound.Play();
            });


            // use car items
            _signalBus.Subscribe<UseItemSignal>((signal) => OnUseItem(signal.Item));


        }

        void Update()
        {
            _engineSound.pitch = Mathf.Lerp(_engineSound.pitch, _enginePitch, 0.1f);
        }

        public void Start()
        {
            _playerSprite = GetComponentInChildren<SpriteRenderer>();
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _engineSound = GetComponent<AudioSource>();

            _signalBus.Fire(new UpdateStatSignal(StatType.Car, Car));
            _signalBus.Fire(new UpdateStatSignal(StatType.Fuel, Fuel));
        }


        public void OnUseItem(Item item)
        {
            if (!_inCar)
                return;

            if (item.ItemType == ItemType.SparePart && Car < 1.0f)
            {
                Car += 0.15f;
                Car = Mathf.Clamp(Car, 0, 1);
                _signalBus.Fire(new UpdateStatSignal(StatType.Car, Car));
                Object.Destroy(item.gameObject);
            }

            if (item.ItemType == ItemType.FuelCan && Fuel < 1.0f)
            {
                Fuel += 0.15f;
                Fuel = Mathf.Clamp(Fuel, 0, 1);
                _signalBus.Fire(new UpdateStatSignal(StatType.Fuel, Fuel));
                Object.Destroy(item.gameObject);
            }
        }

        void FixedUpdate()
        {
            if (_looting || (_inCar && Fuel <= 0.0f) || (_inCar && Car <= 0.0f))
            {
                _rigidbody2D.velocity = Vector2.zero;
                return;
            }

            var speed = _playerSprite == _inCar ? CarSpeed : PlayerSpeed;
                
            var currentRoadPositionToTravel = Road.GetPosition(_currentRoadNode);

            var delta = (currentRoadPositionToTravel - transform.position);
            var distance = delta.magnitude;
            var direction = delta.normalized * speed;

            _rigidbody2D.velocity = direction * Time.fixedDeltaTime;

            if (distance < 0.1)
            {
                IncrementRoadNode(_direction);
            }

            if (_currentRoadNode == Road.positionCount)
                _signalBus.Fire(new EndGameSignal(EndGameType.Won));

            Fuel -= direction.magnitude * FuelBurnRate * Time.fixedDeltaTime;
            Fuel = Mathf.Clamp(Fuel, 0f, 1f);
            _signalBus.Fire(new UpdateStatSignal(StatType.Fuel, Fuel));

            Car -= 0.003f * Time.fixedDeltaTime;
            Car = Mathf.Clamp(Car, 0f, 1f);
            _signalBus.Fire(new UpdateStatSignal(StatType.Car, Car));

        }

        void IncrementRoadNode(DirectionType currentDirection)
        {
            if (currentDirection == DirectionType.East)
                _currentRoadNode++;
            else
                _currentRoadNode--;

            _currentRoadNode = Mathf.Clamp(_currentRoadNode, 0, Road.positionCount);
        }
    }
}
