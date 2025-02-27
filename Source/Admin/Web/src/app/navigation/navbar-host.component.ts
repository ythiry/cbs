import React, { createElement } from 'react';
import ReactDOM, { render } from 'react-dom';
import {Component, OnInit, AfterContentInit, ViewEncapsulation} from '@angular/core';
import CBSNavigation from 'navigation/lib/index.js';

@Component({
  selector: 'navbar-host',
  templateUrl:'./navbar-host.component.html',
  styleUrls: ['../../../node_modules/navigation/lib/cbs-navigation.scss'],
  encapsulation: ViewEncapsulation.None
})

export class NavbarHostComponent implements AfterContentInit, OnInit {
  static apiBaseUrl: string;
    
    username: string;  

    constructor() {
      this.username = 'Unknown';
    }

    ngOnInit(){
      fetch(`${NavbarHostComponent.apiBaseUrl}/identity`).then(async response => this.username = await response.text());
    }

    ngAfterContentInit(){
        render(createElement(CBSNavigation, {activeMenuItem: 'admin'}), document.getElementById("top-navbar"));
    }
}