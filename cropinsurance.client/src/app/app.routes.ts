import { Routes } from '@angular/router';
import { Seasons } from './seasons/seasons';
import { Crops } from './crops/crops';
import { CropDetails } from './cropdetails/cropdetails';
import { Applications } from './applications/applications';

export const routes: Routes = [
    {
        path: '',
        pathMatch: 'full',
        redirectTo: 'applications'
    },
    {
        path: 'applications',
        component: Applications
    },
    {
        path: 'seasons',
        component: Seasons,
        children: [
            {
                path: ':seasonId/crops',
                component: Crops,
                children: [
                    {
                        path: ':cropId',
                        component: CropDetails
                    }
                ]
            }
        ]
    }
];
