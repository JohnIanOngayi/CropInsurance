import { Component } from '@angular/core';
import { OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Cropsservice } from '../services/cropsservice/cropsservice';
import { Crop } from '../models/Crop';

@Component({
  selector: 'app-crops',
  imports: [],
  templateUrl: './crops.html',
  styleUrl: './crops.css',
})
export class Crops implements OnInit {
  constructor(private cropsService: Cropsservice, private route: ActivatedRoute) {
  }

  crops: Crop[] = [];
  seasonId: number = 0;;

  ngOnInit() {
    this.seasonId = this.route.snapshot.params['seasonId'];
    this.loadCrops();
  }


  loadCrops() {
    this.cropsService.getCropsBySeason(this.seasonId).subscribe(
      (data: Crop[]) => {
        console.log('Crops Loaded: ', data)
        this.crops = data;
      },
      (error: any) => {
        console.error('Error fetching seasons:', error);
      }
    );
  }
}
