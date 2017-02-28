using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MvvmNano.Forms;
using MvvmNanoDemo.ViewModels;
using Xamarin.Forms; 

namespace MvvmNanoDemo.Pages
{
    public class SecondDetailPage : MvvmNanoContentPage<SecondDetailViewModel>
    {   
        private Grid _contentGrid = new Grid();

        private List<Block> Blocks = new List<Block>();

        public override void OnViewModelSet()
        {
            base.OnViewModelSet(); 

            Content = _contentGrid;
        }
         
        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);

            if (!Blocks.Any())
                AddBlock((int)Width/2, (int)Height/2);
        } 

        private void AddBlock(int x, int y)
        { 
            if (Blocks.Count == 100)
            {
                _contentGrid.Children.Remove(Blocks[0]);
                Blocks.RemoveAt(0);
            }

            var random = new Random((int) DateTime.Now.Ticks);
            var randomColor = Color.FromRgb(random.Next(0, 255), random.Next(0, 255), random.Next(0, 255));

            var block = new Block(this, randomColor);
            block.Move(x,y);

            Blocks.Add(block);
            _contentGrid.Children.Add(block); 

            new Animation
            {
                { 0, 0.5, new Animation (v => _contentGrid.BackgroundColor = randomColor.MultiplyAlpha(v * 2)) },
                { 0.5, 1, new Animation (v => _contentGrid.BackgroundColor = randomColor.MultiplyAlpha(2- v * 2)) }
            }.Commit(this, "flash", easing: Easing.CubicInOut);
        }

        class Block : BoxView
        {
            private SecondDetailPage _parent;

            private int _lastX;
            private int _lastY;
            private int size = 20; 
            private bool _didCreateAChild; 

            public Block(SecondDetailPage parent, Color color)
            {
                _parent = parent;
                RandomMove();
                Color = color; 
            }

            public void Move(int x, int y)
            { 
                int right = (int)_parent.Width - x - size;
                int bottom = (int)_parent.Height - y - size;

                Margin = new Thickness(x, y, right, bottom);

                _lastX = x;
                _lastY = y;
            }

            private void RandomMove()
            {
                var random = new Random((int)DateTime.Now.Ticks); 
                int x = random.Next(size, (int) _parent.Width - 20);
                int y = random.Next(size, (int) _parent.Height - 20); 
                
                var animation = new Animation(d =>
                {
                    var newX = _lastX + (x > _lastX ? x - _lastX : -(_lastX - x)) * d;
                    var newY = _lastY + (y > _lastY ? y - _lastY : -(_lastY - y)) * d;
                    Move((int)newX, (int)newY);
                });

                
                animation.Commit(this, "MoveAnimation", length: 1500, easing: Easing.CubicInOut, finished: (d, b) =>
                {
                    _lastX = x;
                    _lastY = y;

                    if (!_didCreateAChild)
                    {
                        _parent.AddBlock(x, y);
                        _didCreateAChild = true;
                    } 
                    RandomMove();
                });
            } 
        }
    }
}