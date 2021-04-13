import Calendar, { IEventScheduleObject, ISchedule } from 'tui-calendar';
import "tui-calendar/dist/tui-calendar.css";
import "bootstrap/dist/css/bootstrap.min.css"

// If you use the default popups, use this.
import 'tui-date-picker/dist/tui-date-picker.css';
import 'tui-time-picker/dist/tui-time-picker.css';
import { Schedulee } from './Model/Schedulee';

var cal = new Calendar('#calendar', {
    useCreationPopup: true,
    defaultView: 'week', // set 'week' or 'day'
    taskView: true,  // e.g. true, false, or ['task', 'milestone'])
    scheduleView: ['time']  // e.g. true, false, or ['allday', 'time'])
});

cal.on('beforeCreateSchedule', async function (event: ISchedule) {
    event.category = 'time';
    cal.createSchedules([event]);

    if (!event.title) {
        alert("No title");
        return;
    }

    const formData = new FormData();

    let poco = new Schedulee(event.title);

    let key: keyof Schedulee;
    for (key in poco) {
        formData.append(key, poco[key]);
    }

    let result = await fetch('home/addschedule', {
        method: 'POST',
        body: formData,
    })


    if (!result.ok) {
        alert('Adding probably failed. Try refreshing the page.')
    }
});

let lastClickSchedule: ISchedule | null = null;
cal.on('clickSchedule', function (event: IEventScheduleObject) {
    var schedule = event.schedule;

    if (lastClickSchedule) {
        if (!lastClickSchedule.id) {
            alert('sanity check fail, lastClickSchedule.id is null');
        } else if (!lastClickSchedule.calendarId) {
            alert('sanity check fail, lastClickSchedule.calendarId is null');
        } else {
            cal.updateSchedule(lastClickSchedule.id, lastClickSchedule.calendarId, {
                isFocused: false
            });
        }
    }

    if (schedule.id == null) {
        alert('sanity check fail, schedule.id is null');
    } else if (schedule.calendarId == null) {
        alert('sanity check fail, schedule.calendarId is null');
    } else {
        alert('event added2');
        cal.updateSchedule(schedule.id, schedule.calendarId, {
            isFocused: true
        });
    }

    lastClickSchedule = schedule;
    alert(`lastClickSchedule.id is ${lastClickSchedule.id}`);
    // open detail view
});