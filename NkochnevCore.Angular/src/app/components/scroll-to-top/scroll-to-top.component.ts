import { Component, HostListener, Input, OnInit } from '@angular/core';
import { animate, state, style, transition, trigger } from '@angular/animations';

@Component({
  selector: 'scroll-to-top',
  templateUrl: './scroll-to-top.component.html',
  styleUrls: ['./scroll-to-top.component.css'],
  animations: [
    trigger('appearInOut',
      [
        state('in',
          style({
            'display': 'block',
            'opacity': '0.85'
          })),
        state('out',
          style({
            'display': 'none',
            'opacity': '0'
          })),
        transition('in => out', animate('400ms ease-in-out')),
        transition('out => in', animate('400ms ease-in-out'))
      ])
  ]
})
export class ScrollToTopComponent implements OnInit {

  private timerID: any = null;
  private animate: boolean = true;
  private speed: number = 80;
  private acceleration: number = 2;
  constructor() { }

  ngOnInit() {
    
  }

  scrollTop(event: any): void {

    if (!this.isBrowser()) {
      return;
    }

    event.preventDefault();
    if (this.animate) {
      this.animateScrollTop();
    } else {
      window.scrollTo(0, 0);
    }
  };

  animateScrollTop() {
    if (this.timerID !== null) {
      return;
    }

    var initialSpeed = this.speed;
    var that = this;
    this.timerID = setInterval(function () {
      window.scrollBy(0, -initialSpeed);
      initialSpeed = initialSpeed + that.acceleration;
      if (that.getCurrentScrollTop() === 0) {
        clearInterval(that.timerID);
        that.timerID = null;
      }
    }, 15);
  };

  /**
     * Get current Y scroll position
     * @returns {number}
     */
  getCurrentScrollTop() {
    if (typeof window.scrollY !== 'undefined' && window.scrollY >= 0) {
      return window.scrollY;
    }

    if (typeof window.pageYOffset !== 'undefined' && window.pageYOffset >= 0) {
      return window.pageYOffset;
    }

    if (typeof document.body.scrollTop !== 'undefined' && document.body.scrollTop >= 0) {
      return document.body.scrollTop;
    }

    if (typeof document.documentElement.scrollTop !== 'undefined' && document.documentElement.scrollTop >= 0) {
      return document.documentElement.scrollTop;
    }

    return 0;
  };

  /**
 * This check will prevent 'window' logic to be executed
 * while executing the server rendering
 * @returns {boolean}
 */
  isBrowser(): boolean {
    return typeof (window) !== 'undefined';
  };

}
