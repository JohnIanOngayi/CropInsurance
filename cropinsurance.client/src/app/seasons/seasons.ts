import { Component } from '@angular/core';
import { Seasonsservice } from '../services/seasonsservice/seasonsservice';
import { Season } from '../models/Season';
import { OnInit } from '@angular/core';

@Component({
  selector: 'app-seasons',
  imports: [],
  templateUrl: './seasons.html',
  styleUrl: './seasons.css',
})
export class Seasons implements OnInit {

  constructor(private seasonsService: Seasonsservice) {
  }

  seasons: Season[] = [];

  ngOnInit() {
    this.loadSeasons();
  }

  loadSeasons() {
    this.seasonsService.getSeasons().subscribe(
      (data) => {
        console.log('Seasons Loaded: ', data)
        this.seasons = data;
      },
      (error) => {
        console.error('Error fetching seasons:', error);
      }
    );
  }
}
