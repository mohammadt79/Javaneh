import { Component } from '@angular/core';
import { RouterLink, RouterLinkActive } from '@angular/router';

@Component({
  selector: 'app-header',
  imports: [RouterLink, RouterLinkActive], // هر دو لازم است
  templateUrl: './header.html',
  styleUrl: './header.scss',
})
export class Header {

  navItems = [
    { label: 'خانه', link: '/' },
    { label: 'آموزش دروس', link: '/lessons' },
    { label: 'آزمون', link: '/exams' },
    { label: 'درباره ما', link: '/about' },
  ];
}
